using System;

namespace SFP.SIT.SERVICES.Model.Adm
{
    public class AdmPerfilMdl
    {
        public Int32 kp_claperfil { get; set; }
        public String kp_descripcion { get; set; }
        public DateTime kp_fecbaja { get; set; }
        public String kp_sigla { get; set; }
        public Int32 kp_multiple { get; set; }

        public AdmPerfilMdl() { }
        public AdmPerfilMdl( Int32 kp_claperfil, String kp_descripcion, DateTime kp_fecbaja, String kp_sigla, Int32 kp_multiple)
        {
            this.kp_claperfil = kp_claperfil;
            this.kp_descripcion = kp_descripcion;
            this.kp_fecbaja = kp_fecbaja;
            this.kp_sigla = kp_sigla;
            this.kp_multiple = kp_multiple;
        }
    }
}
