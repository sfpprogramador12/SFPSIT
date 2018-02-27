using System;

namespace SFP.SIT.SESAI.Models
{
    public class SesaiTurnadaMdl
    {
        public Int64 no_folio { get; set; }
        public Int64 id_turnada { get; set; }
        public DateTime fechaturno { get; set; }
        public Int32 id_usuario_origen { get; set; }
        public String observaciones { get; set; }
        public String inf_completa { get; set; }
        public String estado { get; set; }
        public Int32 id_area_destino { get; set; }
        public Int32 id_area_origen { get; set; }
        public String canalizada { get; set; }
        public String competencia { get; set; }
        public String res_ci { get; set; }
        public Int64 id_turnadaant { get; set; }
        public String desfazada { get; set; }
        public DateTime fecha_cancelada { get; set; }
        public int indice { get; set; }
    }
}
