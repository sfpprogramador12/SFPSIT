using System;

namespace SFP.SIT.SERVICES.Model.Snt
{
    public class SntEstadoMdl
    {
        public Int32 ke_claest { get; set; }
        public Int32 kpa_clapai { get; set; }
        public String ke_descripcion { get; set; }
        public DateTime ke_fecbaja { get; set; }

        public SntEstadoMdl() { }

        public SntEstadoMdl(Int32 ke_claest, Int32 kpa_clapai, String ke_descripcion, DateTime ke_fecbaja)
        {
            this.ke_claest = ke_claest;
            this.kpa_clapai = kpa_clapai;
            this.ke_descripcion = ke_descripcion;
            this.ke_fecbaja = ke_fecbaja;
        }
    }
}
