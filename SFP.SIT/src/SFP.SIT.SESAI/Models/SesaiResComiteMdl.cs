using System;

namespace SFP.SIT.SESAI.Models
{
    public class SesaiResComiteMdl
    {
        public Int64 id_turnada { get; set; }
        public String no_folio { get; set; }
        public String resuelto { get; set; }
        public DateTime fecha { get; set; }
        public String conf_rev { get; set; }
        public String observacion { get; set; }
        public String motivo { get; set; }
        public String aut_amp_tiempo { get; set; }
        public String no_oficio { get; set; }
        public DateTime fecha_oficio { get; set; }
        public String archivo { get; set; }
    }
}
