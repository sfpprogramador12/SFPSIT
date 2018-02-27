using SFP.SIT.SERV.Model.DOC;
using SFP.SIT.SERV.Model.RESP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFP.SIT.WEB.Models
{
    public class FrmRespConfidencialVM : RespBaseVM
    {
        public SIT_RESP_RESPUESTA resRespuesta { set; get; }
        // Elemento
        public DocContenidoMdl docResolucionMdl { set; get; }
        public string DatosJson { set; get; }

        // 1.- Es la respuesta
        // 2.- Es el elemento
        public int OperSubTipo { set; get; }

    }
}
