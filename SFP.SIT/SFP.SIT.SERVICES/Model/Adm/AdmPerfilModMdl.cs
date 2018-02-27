using System;

namespace SFP.SIT.SERVICES.Model.Adm
{
    public class AdmPerfilModMdl
    {
        public Int32 kp_claperfil { get; set; }
        public Int32 km_clamodulo { get; set; }

        public AdmPerfilModMdl() { }

        public AdmPerfilModMdl(Int32 kp_claperfil,  Int32 km_clamodulo )
        {
            this.kp_claperfil = kp_claperfil;
            this.km_clamodulo = km_clamodulo;
        }
    }
}
