using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFP.SIT.WEB.Models
{
    public class RespBaseVM
    {
        public int recid { set; get; }
        public int Oper { set; get; }

        public long solclave { get; set; }
        public int proclave { get; set; }
        public long nodclave { get; set; }
        public int araclave { get; set; }
        public long solfecsol { get; set; }
        public long solfecsolticks { get; set; }
        
        // QUITAR LOS NODOS:;.,...,        
        public int avanzar { set; get; }

        public RespBaseVM() { }

        public RespBaseVM(long solclave, int proclave, long nodclave, int araclave, int Oper)
        {
            this.solclave = solclave;
            this.proclave = proclave;
            this.nodclave = nodclave;
            this.araclave = araclave;
            this.Oper = Oper;
        }
    }
}
