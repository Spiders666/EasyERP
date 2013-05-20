﻿using EasyERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyERP.Helpers
{
    public class OrderHelpers
    {
        public static string DisplayStateName(OrderState state)
        {
            string resourceName = "OrderState" + state.ToString();
            return HttpContext.GetGlobalResourceObject("Resources", resourceName).ToString();
        }
        public static string GetStateClass(OrderState state)
        {
            return state.ToString().ToLower();
        }

        public static SelectList GetSelectList()
        {
            var items = new[]
            {
                new SelectListItem { Value = OrderState.NotConfirmed.ToString(), Text = DisplayStateName(OrderState.NotConfirmed) },
                new SelectListItem { Value = OrderState.Canceled.ToString(), Text = DisplayStateName(OrderState.Canceled) },
                new SelectListItem { Value = OrderState.Pending.ToString(), Text = DisplayStateName(OrderState.Pending) },
                new SelectListItem { Value = OrderState.Sent.ToString(), Text = DisplayStateName(OrderState.Sent) }
            };

            return new SelectList(items, "Value", "Text");
        }
    }
}
