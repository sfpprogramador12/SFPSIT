using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SERVICES.Model.Sol
{
    public class SolicitudSegMdl : SolSeguimientoMdl
    {
        public int tso_clatiposol { get; set; }

        public SolicitudSegMdl() { }
        public SolicitudSegMdl (
            long us_clafolio, int krp_claproceso, DateTime seg_fecini, DateTime seg_fecfin, DateTime seg_fecampliacion, int seg_diassemaforo, int seg_colorsemaforo, 
            int seg_multiple, int seg_diasnolaborales, DateTime seg_feccalculo, int kar_clatipoari, int seg_edoproceso, int afd_claafd, int seg_resp_exterior, 
            long nre_claarista, long seg_ultimonodo, DateTime seg_fecestimada, int tso_clatiposol) 
            : base( us_clafolio, krp_claproceso, seg_fecini, seg_fecfin, seg_fecampliacion, seg_diassemaforo, seg_colorsemaforo, seg_multiple, seg_diasnolaborales, 
                 seg_feccalculo, kar_clatipoari, seg_edoproceso, afd_claafd, seg_resp_exterior, nre_claarista, seg_ultimonodo, seg_fecestimada)
        {

            this.tso_clatiposol = tso_clatiposol;
        }        
    }
}
