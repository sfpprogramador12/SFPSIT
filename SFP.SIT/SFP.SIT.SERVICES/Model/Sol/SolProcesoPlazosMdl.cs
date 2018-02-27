using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SERVICES.Model.Sol
{
    public class SolProcesoPlazosMdl
    {
        public int krp_claproceso { get; set; }
        public int tso_clatiposol { get; set; }
        public int kpz_tipoplazo { get; set; }
        public int kpz_plazo { get; set; }
        public int kpz_verde { get; set; }
        public int kpz_amarillo { get; set; }

        public SolProcesoPlazosMdl() { }
        public SolProcesoPlazosMdl( int krp_claproceso, int tso_clatiposol, int kpz_tipoplazo, 
            int kpz_plazo, int kpz_verde, int kpz_amarillo)
        {
            this.krp_claproceso = krp_claproceso;
            this.tso_clatiposol = tso_clatiposol;
            this.kpz_tipoplazo = kpz_tipoplazo;

            this.kpz_plazo = kpz_plazo;
            this.kpz_verde = kpz_verde;
            this.kpz_amarillo = kpz_amarillo;
        }
    }
}
