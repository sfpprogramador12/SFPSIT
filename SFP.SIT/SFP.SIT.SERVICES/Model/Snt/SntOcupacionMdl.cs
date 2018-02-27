using System;

namespace SFP.SIT.SERVICES.Model.Snt
{
    public class SntOcupacionMdl
    {
        public Int32 us_ocupacion { get; set; }
        public String ocu_descripcion { get; set; }
        public DateTime ocu_fecbaja { get; set; }
        public SntOcupacionMdl() { }
    }
}
