using Microsoft.AspNetCore.Http;
using SFP.SIT.SERV.Model.DOC;
using SFP.SIT.SERV.Model.RESP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFP.SIT.WEB.Models
{
    public class FrmRespIncompVM : RespBaseVM
    {
        public SIT_RESP_GRAL respGeneral { set; get; }
        public SIT_RESP_RESPUESTA respRespuesta { get; set; }
        public DocContenidoMdl respDoc { get; set; }
    }
}