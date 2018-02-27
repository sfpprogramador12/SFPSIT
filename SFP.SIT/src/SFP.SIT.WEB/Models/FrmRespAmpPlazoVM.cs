using SFP.SIT.SERV.Model.DOC;
using SFP.SIT.SERV.Model.RESP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFP.SIT.WEB.Models
{
    public class FrmRespAmpPlazoVM : RespBaseVM
    {
        public SIT_RESP_RESPUESTA respRespuesta { set; get; }
        public SIT_RESP_GRAL respGeneral { set; get; }
        public DocContenidoMdl respDoc { set; get; }

        public string descripcion { set; get; }
    }
}
