using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyERP.Models;

namespace EasyERP.Helpers
{
    public static class FlashMessageHelper
    {
        public enum TypeOption
        {
            Notice = 1,
            Information = 2,
            Success = 3,
            Warning = 4,
            Error = 5
        }

        public static void SetMessage(this Controller controller, string message, TypeOption type = TypeOption.Success)
        {
            controller.TempData[string.Format("flash-message-{0}", type.ToString().ToLower())] = message;
        }

        public static MvcHtmlString DisplayMessage(TempDataDictionary tempData)
        {
            var result = tempData.Where(item => item.Key.StartsWith("flash-message-")).Select(item => new { Class = item.Key.Replace("flash-message-", ""), Message = item.Value }).SingleOrDefault();

            TagBuilder closeButton = new TagBuilder("button");
            closeButton.AddCssClass("close");
            closeButton.MergeAttribute("data-dismiss", "alert");
            closeButton.InnerHtml = "&times;";
            
            TagBuilder flashMessage = new TagBuilder("div");
            flashMessage.AddCssClass("alert alert-" + result.Class);
            flashMessage.InnerHtml = closeButton.ToString()
                + result.Message.ToString();

            return MvcHtmlString.Create(flashMessage.ToString());
        }

        public static bool IsMessage(TempDataDictionary tempData)
        {
            var result = tempData.Where(item => item.Key.StartsWith("flash-message-")).Select(item => new { Class = item.Key, Message = item.Value }).SingleOrDefault();
            return result != null && result.Class != null && result.Message != null;
        }
    }
}
