using SFP.SIT.SERV.Model.DOC;
using SFP.SIT.SERV.Model.RESP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFP.SIT.WEB.Models
{
    public class FrmRespInexistenciaVM : RespBaseVM
    {
        public SIT_RESP_RESPUESTA resRespuesta { set; get; }
        public SIT_RESP_INEXISTENCIA resInexistencia { set; get; }

        public SIT_RESP_DETALLE respLugar { set; get; }
        public SIT_RESP_DETALLE respModo { set; get; }
        public SIT_RESP_DETALLE respTiempo { set; get; }

        public DocContenidoMdl docResolucionMdl { set; get; }
        public DocContenidoMdl docLugarMdl { set; get; }
        public DocContenidoMdl docModoMdl { set; get; }
        public DocContenidoMdl docTiempoMdl { set; get; }

    }
}
