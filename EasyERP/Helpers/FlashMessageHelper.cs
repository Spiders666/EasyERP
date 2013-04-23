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
            Success = 1,
            Error = 2
        }

        public static void SetMessage(this Controller controller, string message, TypeOption type = TypeOption.Success)
        {
            controller.TempData[string.Format("flash-message-{0}", type.ToString().ToLower())] = message;
        }

        public static MvcHtmlString DisplayMessage(TempDataDictionary tempData)
        {
            var result = tempData.Where(item => item.Key.StartsWith("flash-message-")).Select(item => new { Class = item.Key, Message = item.Value }).SingleOrDefault();

            TagBuilder tagBuilder = new TagBuilder("div");
            tagBuilder.AddCssClass(result.Class.ToString());
            tagBuilder.SetInnerText(result.Message.ToString());
            return MvcHtmlString.Create(tagBuilder.ToString());
        }

        public static bool IsMessage(TempDataDictionary tempData)
        {
            var result = tempData.Where(item => item.Key.StartsWith("flash-message-")).Select(item => new { Class = item.Key, Message = item.Value }).SingleOrDefault();
            return result != null && result.Class != null && result.Message != null;
        }
    }
}
