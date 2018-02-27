using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SERV.Model.TAB
{
    public class TabConsultaMdl
    {
        public List<string> asTituloX { get; set; }
        public List<string> asTituloY { get; set; }
        public Int32[,] Grafica { get; set; }

        public TabConsultaMdl() { }
    }
}
