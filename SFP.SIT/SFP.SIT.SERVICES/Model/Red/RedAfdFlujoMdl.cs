using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SERVICES.Model.Red
{
    public class RedAfdFlujoMdl
    {
        public int afd_claAfd { get; set; }        
        public int kne_origen { get; set; }
        public int kar_clatipoari { get; set; }
        public int kne_destino { get; set; }
        public int aff_plazo { get; set; }

        public RedAfdFlujoMdl() { }
        public RedAfdFlujoMdl( int claAfd, int origen, int clatipoari,  int destino, int aff_plazo)
        {
            this.afd_claAfd = claAfd;
            this.kne_origen = origen;
            this.kar_clatipoari = clatipoari;
            this.kne_destino = destino;
            this.aff_plazo = aff_plazo;
        }
    }
}
