using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Utils
{
    public class SweetAlerts
    {
        public static string Mensaje(string titulo, string mensaje,SweetAlertMessageType sweetAlertMessageType)
        {
            return "swal({title:'" + titulo + "', text: '" +  mensaje + "', type: '" + sweetAlertMessageType + "'});";
        }

        public enum SweetAlertMessageType
        {
            warning, error, success, info
        }
    }
}