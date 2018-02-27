using System;

namespace SFP.SIT.SESAI.Models
{
    public class SesaiRevFormaMdl
    {
        public Int64 id_turnada { get; set; }
        public String no_folio { get; set; }
        public String resuelto { get; set; }
        public DateTime fecha { get; set; }
        public String conf_rev { get; set; }
        public String observacion { get; set; }
    }
}
