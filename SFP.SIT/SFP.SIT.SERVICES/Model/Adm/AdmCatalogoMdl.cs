using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SERVICES.Model.Adm
{
    public class AdmCatalogoMdl
    {
        public Int32 cat_clacat { get; set; }
        public string cat_descripcion { get; set; }
        public string cat_clase { get; set; }
        public Int32 kp_claperfil { get; set; }

        public AdmCatalogoMdl() { }
        public AdmCatalogoMdl(Int32 cat_clacat, string cat_descripcion, string cat_clase, Int32 kp_claperfil)
        {
            this.cat_clacat = cat_clacat;
            this.cat_descripcion = cat_descripcion; 
            this.cat_clase = cat_clase;
            this.kp_claperfil = kp_claperfil;
        }

    }
}
