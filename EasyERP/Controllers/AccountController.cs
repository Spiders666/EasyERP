using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using EasyERP.Models;
using EasyERP.Filters;
using System.Net;
using System.Net.Mail;

namespace EasyERP.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Nazwa użytkownika lub hasło są niepoprawne.");
            return View(model);
        }
        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Home");
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                    WebSecurity.Login(model.UserName, model.Password);
                    Roles.AddUserToRole(model.UserName, UserRole.User);
                    return RedirectToAction("Register2", "Account");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register2

        public ActionResult Register2()
        {
            return View();
        }

        //
        // POST: /Account/Register2

        [HttpPost]
        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Register2(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var Query = from m in db.Customers
                            where m.Email == customer.Email
                            select m;

                // Attempt to fill customer data
                if (!Query.Any())
                {
                    customer.UserId = WebSecurity.CurrentUserId;
                    customer.ActivationLink = System.IO.Path.GetRandomFileName().Replace(".", string.Empty);
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    return RedirectToAction("SendEmail", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "W naszej bazie danych już istnieje konto z takim adresem email.");
                }
                // If we got this far, something failed, redisplay form
            }
            return View();
        }
        //
        // POST: /Account/Disassociate

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Disassociate(string provider, string providerUserId)
        {
            string ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId);
            ManageMessageId? message = null;

            // Only disassociate the account if the currently logged in user is the owner
            if (ownerAccount == User.Identity.Name)
            {
                // Use a transaction to prevent the user from deleting their last login credential
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
                {
                    bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
                    if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1)
                    {
                        OAuthWebSecurity.DeleteAccount(provider, providerUserId);
                        scope.Complete();
                        message = ManageMessageId.RemoveLoginSuccess;
                    }
                }
            }

            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage

        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Twoje hasło zostało zmienione."
                : message == ManageMessageId.SetPasswordSuccess ? "Twoje hasło zostało ustawione."
                : message == ManageMessageId.RemoveLoginSuccess ? "Zewnętrzny login skasowany."
                : "";
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        public ActionResult Manage2()
        {
            var CustomerId = Helpers.AccountHelpers.GetCustomerId();
            var query = from c in db.Customers
                        where c.Id == CustomerId
                        select c;
            var GetQuery = query.FirstOrDefault();

            if (GetQuery == null)
            {
                return View();
            }
            return View(GetQuery);
        }

        [HttpPost]
        [ValidateInput(true)]
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Manage2(Customer customer)
        {
            customer.Id = Helpers.AccountHelpers.GetCustomerId();
            if (ModelState.IsValid)
            {
                // Attempt to fill customer data
                db.Entry(customer).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Manage2", "Account");
            }
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Zmiana hasła nie powiodła się.");
                    }
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
            if (!result.IsSuccessful)
            {
                return RedirectToAction("ExternalLoginFailure");
            }

            if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
            {
                return RedirectToLocal(returnUrl);
            }

            if (User.Identity.IsAuthenticated)
            {
                // If the current user is logged in add the new account
                OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // User is new, ask for their desired membership name
                string loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
                ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
                ViewBag.ReturnUrl = returnUrl;
                return View("ExternalLoginConfirmation", new RegisterExternalLoginModel { UserName = result.UserName, ExternalLoginData = loginData });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
        {
            string provider = null;
            string providerUserId = null;

            if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Insert a new user into the database
                using (DatabaseContext db = new DatabaseContext())
                {
                    UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());
                    // Check if user already exists
                    if (user == null)
                    {
                        // Insert name into the profile table
                        db.UserProfiles.Add(new UserProfile { UserName = model.UserName });
                        db.SaveChanges();

                        OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName);
                        OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false);

                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("UserName", "Nazwa użytkownika już istnieje. Proszę wybrać inną.");
                    }
                }
            }

            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // GET: /Account/ExternalLoginFailure

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ExternalLoginsList(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData);
        }

        [ChildActionOnly]
        public ActionResult RemoveExternalLogins()
        {
            ICollection<OAuthAccount> accounts = OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name);
            List<ExternalLogin> externalLogins = new List<ExternalLogin>();
            foreach (OAuthAccount account in accounts)
            {
                AuthenticationClientData clientData = OAuthWebSecurity.GetOAuthClientData(account.Provider);

                externalLogins.Add(new ExternalLogin
                {
                    Provider = account.Provider,
                    ProviderDisplayName = clientData.DisplayName,
                    ProviderUserId = account.ProviderUserId,
                });
            }

            ViewBag.ShowRemoveButton = externalLogins.Count > 1 || OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            return PartialView("_RemoveExternalLoginsPartial", externalLogins);
        }
        // Get: /Account/ResetForm
        [AllowAnonymous]
        public ActionResult ResetForm()
        {
            return View();
        }
        // Post: /Account/ResetForm
        [AllowAnonymous]
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ResetForm(FormCollection formCollection)
        {
            string GetEmail = formCollection["email"].ToString();
            var Query = from m in db.Customers
                        where m.Email == GetEmail
                        select m;

            var SiteAddress = Request.Url.Authority;
            var GetQuery = Query.FirstOrDefault();

            if (Query.FirstOrDefault() == null)
            {
                ModelState.AddModelError("","Nie ma takiego adresu email w bazie danych!");
            } 
            else 
            {
                ModelState.AddModelError("", "Adres poprawny, email resetujacy został wysłany!");

                // generation of new reset link
                Customer customer = db.Customers.Single(p => p.Email == GetEmail);
                customer.ActivationLink = System.IO.Path.GetRandomFileName().Replace(".", string.Empty);
                db.SaveChanges();
                //sending new link trough email
                var subject = "Easy ERP - Formularz resetu hasła";
                var fromAddress = new MailAddress("easyerp@o2.pl", "Easy ERP");
                var toAddress = new MailAddress(GetQuery.Email, GetQuery.Name + " " + GetQuery.SurName);
                const string fromPassword = "easyerp_123";
                var smtp = new SmtpClient
                {
                    Host = "poczta.o2.pl",
                    Port = 587,
                    Timeout = 30000,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    IsBodyHtml = true,
                    Body = "<h1>Easy ERP</h1><h4>Witaj " + GetQuery.Name + " " + GetQuery.SurName + "</h4>Twój link resetu: <a target==\"_blank\" href=\"" + SiteAddress + "/Account/Reset/" + GetQuery.ActivationLink + "\">" + SiteAddress + "/Account/Reset/" + GetQuery.ActivationLink + "</a>"
                })
                {
                    smtp.Send(message);
                }
            }
            return View(formCollection);
        }
        // Get: /Account/Reset
        [AllowAnonymous]
        public ActionResult Reset(string id)
        {
            var Query = from m in db.Customers
                        where m.ActivationLink == id
                        select m;
            var GetQuery = Query.FirstOrDefault();
            ViewBag.ReturnMsg = "";
            ViewBag.Allow = 0;
            if (GetQuery != null)
            {
                ViewBag.Allow = 1;
            }
            return View();
        }

        // Post: /Account/Reset
        [AllowAnonymous]
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Reset(FormCollection formCollection)
        {
            string GetEmail = formCollection["Email"].ToString();
            string GetPassword = formCollection["Password"].ToString();
            string GetRepeatPassword = formCollection["RepeatPassword"].ToString();

            var Query = from m in db.Customers
                        where m.Email == GetEmail
                        select m;
            var GetCustomer = Query.FirstOrDefault();
            if (Query.FirstOrDefault() == null)
            {
                ModelState.AddModelError("", "Nie ma takiego adresu email w bazie danych!");
                return View(formCollection);
            }
            else
            {
                if (GetPassword != GetRepeatPassword)
                {
                    ModelState.AddModelError("", "Hasła się nie zgadzają!");
                }
                else
                {
                    var QueryUserName = from a in db.UserProfiles
                                        where a.UserId == GetCustomer.UserId
                                        select a.UserName;
                    var GetUserName = QueryUserName.FirstOrDefault();
                    var token = WebSecurity.GeneratePasswordResetToken(GetUserName, 1);
                    var result = WebSecurity.ResetPassword(token, GetPassword);
                    Customer customer = db.Customers.Single(p => p.ActivationLink == GetCustomer.ActivationLink);
                    customer.ActivationLink = null;
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
            }
            return View(formCollection);
        }
        // Get: /Account/SendEmail
        public ActionResult SendEmail()
        {
            // id=1 activation, id=2 password reset
            var CustomerId = Helpers.AccountHelpers.GetCustomerId();
            string subject = "Easy ERP - Formularz aktywacji konta";
            Customer customer = db.Customers.Single(p => p.Id == CustomerId);
            customer.ActivationLink = System.IO.Path.GetRandomFileName().Replace(".", string.Empty);
            db.SaveChanges();

            var Query = from m in db.Customers
                        where m.Id == CustomerId
                        select m;

            // zrobic run only on password reset form
            var Address = Request.Url.Authority;
            var GetQuery = Query.FirstOrDefault();

            var fromAddress = new MailAddress("easyerp@o2.pl", "Easy ERP");
            var toAddress = new MailAddress(GetQuery.Email, GetQuery.Name+" "+GetQuery.SurName);
            const string fromPassword = "easyerp_123";

            var smtp = new SmtpClient
            {
                Host = "poczta.o2.pl",
                Port = 587,
                Timeout = 30000,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                IsBodyHtml = true,
                Body = "<h1>Easy ERP</h1>Witaj oto twój link aktywacji: <a target==\"_blank\" href=\"" + Address + "/Account/Activation/" + GetQuery.ActivationLink + "\">" + Address + "/Account/Activation/" + GetQuery.ActivationLink + "</a>"
            })
            {
                smtp.Send(message);
            }
            return View();
        }
        public ActionResult Activation(string id)
        {
            var Query = from m in db.Customers
                        where m.ActivationLink == id
                        select m;
            var GetQuery = Query.FirstOrDefault();
            if (GetQuery == null) {
                ViewBag.ActivationStatus = "Nie ma takiego konta do aktywacji :(";
            } else {
                ViewBag.ActivationStatus = "Konto zostało aktywowane :)";
                Customer customer = db.Customers.Single(p => p.ActivationLink == id);
                customer.ActivationLink = null;
                customer.Activation = true;
                db.SaveChanges();
            }
            return View();
        }
        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Użytkownik z taką nazwą już istnieje.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Użytkownik z takim adresem email już istnieje.";

                case MembershipCreateStatus.InvalidPassword:
                    return "Podane hasło jest niepoprawne.";

                case MembershipCreateStatus.InvalidEmail:
                    return "Podany e-mail jest niepoprawny.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "";

                case MembershipCreateStatus.InvalidQuestion:
                    return "";

                case MembershipCreateStatus.InvalidUserName:
                    return "Niepoprawna nazwa użytkownika, wpisz jeszcze raz.";

                case MembershipCreateStatus.ProviderError:
                    return "Błąd zewnętrznej usługi, skontaktuj się z administratorem.";

                case MembershipCreateStatus.UserRejected:
                    return "Użytkownik odrzucony, skontaktuj się z administratorem.";

                default:
                    return "Nierozpoznany błąd, skontaktuj się z administratorem.";
            }
        }
        #endregion
    }
}
