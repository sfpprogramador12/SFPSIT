using SFP.SIT.SERV.Model.DOC;
using SFP.SIT.SERV.Model.RESP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFP.SIT.WEB.Models
{
    public class FrmRespFinal : RespBaseVM
    {
        public SIT_RESP_RESPUESTA resRespuesta { set; get; }
        public SIT_RESP_GRAL resRespGral { set; get; }
        public DocContenidoMdl docDocumento { set; get; }
        public int rtpclaveAux { get; set; }
        public FrmRespFinal() { }
    }
}