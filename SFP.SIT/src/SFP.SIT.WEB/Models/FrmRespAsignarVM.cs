using SFP.SIT.SERV.Model.RESP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFP.SIT.WEB.Models
{
    public class FrmRespAsignarVM : RespBaseVM
    {
        public SIT_RESP_RESPUESTA resRespuesta { set; get; }
        public SIT_RESP_TURNAR resAsignar { set; get; }

        public  FrmRespAsignarVM() { }
    }
}
