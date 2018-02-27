using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SERVICES.Model.Sol
{
    public class SolProcesoMdl
    {
        public int krp_claproceso { get; set; }
        public String krp_descripcion { get; set; }

        public SolProcesoMdl() { }
        public SolProcesoMdl(int krp_claproceso, String krp_descripcion)
        {
            this.krp_claproceso = krp_claproceso;
            this.krp_descripcion = krp_descripcion;
        }
    }
}
