using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SERV.Dao.RESP
{
    public class NodoRespuestaMdl
    {
        public int recid { set; get; }
        public int rtpclave { set; get; }
        public int repclave { set; get; }
        public string rtpdescripcion { set; get; }
        public string docfolio { set; get; }
        public int docclave { set; get; }
        public int repcantidad { set; get; }
        public string datdescripcion { set; get; }
        public string megdescripcion { set; get; }
        public string rtpforma { set; get; }

        public string dattitulo { set; get; }
    public NodoRespuestaMdl() { }
    }
}
