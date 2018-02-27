using System;

namespace SFP.SIT.SERVICES.Model.Adm
{
    public class AdmTipoAreaMdl
    {
        public Int32 kta_clatipo_area { get; set; }
        public String kta_siglas { get; set; }
        public String kta_descripcion { get; set; }
        public DateTime kta_fecbaja { get; set; }

        public AdmTipoAreaMdl() { }
        public AdmTipoAreaMdl(Int32 kta_clatipo_area, String kta_siglas, String kta_descripcion, DateTime kta_fecbaja)
        {
            this.kta_clatipo_area = kta_clatipo_area;
            this.kta_siglas = kta_siglas;
            this.kta_descripcion = kta_descripcion;
            this.kta_fecbaja = kta_fecbaja;
        }
    }
}
