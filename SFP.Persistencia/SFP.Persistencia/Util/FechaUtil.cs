using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.Persistencia.Util
{
    public static class FechaUtil
    {
        public static DateTime Fecha(string dato)
        {
            IFormatProvider culture = new System.Globalization.CultureInfo("es-MX", true);
            return DateTime.Parse(dato, culture, System.Globalization.DateTimeStyles.AssumeLocal);
        }


        // yyyy-MM-dd  --> DateTime
        public static DateTime AsignarFecha(string sFecha)
        {
            if (sFecha != "" && sFecha != "undefined")
            {
                int iAño = Convert.ToInt32(sFecha.Substring(0, 4));
                int iMes = Convert.ToInt32(sFecha.Substring(5, 2));
                int iDia = Convert.ToInt32(sFecha.Substring(8, 2));
                return new DateTime(iAño, iMes, iDia);
            }
            else
                return new DateTime();
        }
    }
}
