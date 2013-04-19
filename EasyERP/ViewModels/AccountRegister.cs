using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyERP.Models;

namespace EasyERP.ViewModels
{
    public class AccountRegister
    {
        public RegisterModel RegisterModel { get; set; }
        public Customer Customer { get; set; }

        public AccountRegister() {}

        public AccountRegister(RegisterModel RegisterModel, Customer Customer)
        {
            this.RegisterModel = RegisterModel;
            this.Customer = Customer;
        }
    }
}