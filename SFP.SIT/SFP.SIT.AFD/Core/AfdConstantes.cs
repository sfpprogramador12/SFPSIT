using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.AFD.Core
{
    public class AfdConstantes
    {
        public static class ACCION
        {
            public const int NO_EJECUTAR = 1;
            public const int EJECUTAR = 0;
        }

        public static class ARISTA_SUBCLASIFICAR
        {
            public const int NO = 0;
            public const int SI = 1;
        }
        public static class ARISTA_TIPO
        {
            public const int INTERNO = 0;
            public const int EXTERNO = 1;
        }



        public static class MULTIPLE
        {
            public const int NO = 0;
            public const int SI = 1;
        }

        public static class NODO
        {
            public const int EN_PROCESO = 0;
            public const int FINALIZADO = 1;
            public const int INDETERMINADO = 2;
        }

        // LAS CLASES DE ESTADO Y ARISTA DEBEMOS DE ENCONTRAR UN VALOR PARA EXTRAER DE LA BASE DE DATOS CON UNA ETIQUETA
        // LAS CLASES DE ESTADO Y ARISTA DEBEMOS DE ENCONTRAR UN VALOR PARA EXTRAER DE LA BASE DE DATOS CON UNA ETIQUETA
        // LAS CLASES DE ESTADO Y ARISTA DEBEMOS DE ENCONTRAR UN VALOR PARA EXTRAER DE LA BASE DE DATOS CON UNA ETIQUETA

        public static class PROCESO_ESTADO
        {
            public const int EN_EJECUCION = 1;
            public const int TERMINADO = 2;
            public const int CANCELADO = 2;
        }

        public static class FECHA
        {
            public static DateTime CrearFechaTurno(Int64 lTurno)
            {
                String sTurno = lTurno.ToString();
                int iAño = Convert.ToInt32(sTurno.Substring(0, 4));
                int iMes = Convert.ToInt32(sTurno.Substring(4, 2));
                int iDia = Convert.ToInt32(sTurno.Substring(6, 2));
                int iHora = Convert.ToInt32(sTurno.Substring(8, 2));
                int iMin = Convert.ToInt32(sTurno.Substring(10, 2));
                int iSeg = Convert.ToInt32(sTurno.Substring(12, 2));

                if (lTurno > 0)
                    return new DateTime(iAño, iMes, iDia, iHora, iMin, iSeg);
                else
                    return new DateTime();
            }
        }

    }
}
