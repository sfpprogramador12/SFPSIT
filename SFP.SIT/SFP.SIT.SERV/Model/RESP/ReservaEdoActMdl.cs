using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SERV.Model.RESP
{
    public class ReservaEdoActMdl
    {
        public long repclave { get; set; }
        public string rsvexpediente { get; set; }
        public int sdoClaveActual { get; set; }

        public ReservaEdoActMdl() { }

        public ReservaEdoActMdl(long repclave, string rsvexpediente, int sdoClaveActual )
        {
            this.repclave = repclave;
            this.rsvexpediente = rsvexpediente;
            this.sdoClaveActual = sdoClaveActual;
        }

    }
}
