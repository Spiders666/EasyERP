using EasyERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyERP.App_GlobalResources;

namespace EasyERP.Helpers
{
    public class OrderHelpers
    {
        public static string DisplayStateName(OrderState state)
        {
            string resourceName = "OrderState" + state.ToString();
            switch (state)
            {
                case OrderState.Canceled:
                    return Resources.OrderStateCanceled;
                case OrderState.NotConfirmed:
                    return Resources.OrderStateNotConfirmed;
                case OrderState.Pending:
                    return Resources.OrderStatePending;
                case OrderState.Sent:
                    return Resources.OrderStateSent;
                default:
                    return "";
            }
        }
        public static string GetStateClass(OrderState state)
        {
            var Classes = "";
            if (state == OrderState.Canceled)
                Classes = "alert alert-error";
            if (state == OrderState.NotConfirmed)
                Classes = "alert alert-info";
            if (state == OrderState.Pending)
                Classes = "alert alert-info";
            if (state == OrderState.Sent)
                Classes = "alert alert-success";
            return Classes;
        }

        public static SelectList GetSelectList()
        {
            var items = new[]
            {
                new SelectListItem { Value = "", Text = "-- status zamówienia --" },
                new SelectListItem { Value = OrderState.NotConfirmed.ToString(), Text = DisplayStateName(OrderState.NotConfirmed) },
                new SelectListItem { Value = OrderState.Canceled.ToString(), Text = DisplayStateName(OrderState.Canceled) },
                new SelectListItem { Value = OrderState.Pending.ToString(), Text = DisplayStateName(OrderState.Pending) },
                new SelectListItem { Value = OrderState.Sent.ToString(), Text = DisplayStateName(OrderState.Sent) }
            };

            return new SelectList(items, "Value", "Text");
        }
    }
}
