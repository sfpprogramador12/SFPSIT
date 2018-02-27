using System;

namespace SFP.SIT.SERVICES.Model.Adm
{
    public class AdmConfiguracionMdl
    {
        public Int32 con_claconfiguracion { get; set; }
        public String con_clave { get; set; }
        public String con_valor { get; set; }
        public DateTime con_fecbaja { get; set; }

        public AdmConfiguracionMdl() { }
        public AdmConfiguracionMdl( Int32 con_claconfiguracion,  String con_clave, String con_valor, DateTime con_fecbaja)
        {
            this.con_claconfiguracion = con_claconfiguracion;
            this.con_clave = con_clave;
            this.con_valor = con_valor;
            this.con_fecbaja = con_fecbaja;
        }
    }
}
