using System;

namespace SFP.SIT.SESAI.Models
{
    public class SesaiSolicitudMdl
    {
        public String no_folio { get; set; }
        public Int32 metclave { get; set; }
        public DateTime fecha_reg_sfp { get; set; }
        public DateTime fecha_recepcion_sisi { get; set; }
        public DateTime fecha_captura_solicitud { get; set; }
        public String info_buscar { get; set; }
        public String arch_desc { get; set; }
        public String otra_modalidad { get; set; }
        public String otros_datos { get; set; }
        public Int32 plazo { get; set; }
        public Int32 id_medio_entrega { get; set; }
        public String arch_acla { get; set; }
        public DateTime fecha_acla { get; set; }
        public String aclaracion { get; set; }
        public String texto_acla { get; set; }
        public Int32 id_tpo_solicitud { get; set; }
    }
}
