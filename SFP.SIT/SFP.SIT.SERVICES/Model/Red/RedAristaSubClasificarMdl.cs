using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SERVICES.Model.Red
{
    public class RedAristaSubClasificarMdl
    {
        public int kar_clatipoari { get; set; }
        public long nre_claarista { get; set; }
        public long us_claFolio { get; set; }

        public RedAristaSubClasificarMdl() { }
        public RedAristaSubClasificarMdl(int kar_clatipoari, long nre_claarista, long us_claFolio )
        {
            this.kar_clatipoari = kar_clatipoari;
            this.nre_claarista = nre_claarista;
            this.us_claFolio = us_claFolio; 
        }
    }
}
