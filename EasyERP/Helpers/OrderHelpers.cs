using EasyERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}
