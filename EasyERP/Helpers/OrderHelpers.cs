using EasyERP.Models;
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
            switch (state)
            {
                case OrderState.NOT_CONFIRMED:
                    return "Nie potwierdzone";
                case OrderState.CANCELED:
                    return "Anulowane";
                case OrderState.PENDING:
                    return "Oczekiwane";
                case OrderState.SENT:
                    return "Wysłane";
                default:
                    return null;
            }
        }

        public static SelectList GetSelectList()
        {
            var items = new[]
            {
                new SelectListItem { Value = OrderState.NOT_CONFIRMED.ToString(), Text = DisplayStateName(OrderState.NOT_CONFIRMED) },
                new SelectListItem { Value = OrderState.CANCELED.ToString(), Text = DisplayStateName(OrderState.CANCELED) },
                new SelectListItem { Value = OrderState.PENDING.ToString(), Text = DisplayStateName(OrderState.PENDING) },
                new SelectListItem { Value = OrderState.SENT.ToString(), Text = DisplayStateName(OrderState.SENT) }
            };

            return new SelectList(items, "Value", "Text");
        }
    }
}
