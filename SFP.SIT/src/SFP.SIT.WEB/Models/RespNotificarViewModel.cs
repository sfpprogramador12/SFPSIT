using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFP.SIT.WEB.Models
{
    public class RespNotificarViewModel
    {
        public string respDesc { get; set; }
        public long respDocIdx { get; set; }
        public string respDocNombre { get; set; }
        public string respDocDesc { get; set; }

        public int respTipoArista { get; set; }

        public RespNotificarViewModel(string respDesc, long respDocIdx, string respDocDesc, string respDocNombre, int respTipoArista)
        {
            this.respDesc = respDesc;
            this.respDocIdx = respDocIdx;
            int iPosicion = respDocNombre.IndexOf("_");

            if (iPosicion > 0)
                this.respDocNombre = respDocNombre.Substring(iPosicion + 1);
            else
                this.respDocNombre = respDocNombre;

            this.respDocDesc = respDocDesc.Replace('\n', '\u0020').Replace('\r', '\u0020').Replace('\t', '\u0020');

            this.respTipoArista = respTipoArista;
        }
    }
}
