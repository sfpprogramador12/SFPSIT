using SFP.SIT.SERV.Model.DOC;
using SFP.SIT.SERV.Model.RESP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFP.SIT.WEB.Models
{
    public class FrmRespReservadaVM : RespBaseVM
    {
        //public const int DOC_EXPEDIENTE = 0;
        //public const int DOC_FUNDAMENTO = 1;
        //public const int DOC_JUSTIFICACION = 2;
        //public const int DOC_MOTIVO = 3;

        public SIT_RESP_RESPUESTA resRespuesta { set; get; }
        // RESERVA
        public SIT_RESP_RESERVA resReserva { set; get; }
        public string areaNombre { set; get; }       
        public Dictionary<string, DocContenidoMdl> dicDocumento { set; get; }
        public Dictionary<string, SIT_RESP_DETALLE> dirRespDetalle { set; get; }

        public DocContenidoMdl respDoc { set; get; }

        // CONTENIDO
        //public string fundamentoLegal { set; get; }
        //public string justificacion { set; get; }
        //public string motivos { set; get; }    
        //public string partes { set; get; }
        public string jsonReservaEdo { set; get; }
    }
}
