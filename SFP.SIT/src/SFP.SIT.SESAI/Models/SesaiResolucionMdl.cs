using System;

namespace SFP.SIT.SESAI.Models
{
    public class SesaiResolucionMdl
    {
        public String no_folio { get; set; }
        public Int64 id_turnada { get; set; }
        public Int32 solrespclave { get; set; }
        public Int32 idformadentrega { get; set; }
        public Int32 idtipoinfo { get; set; }
        public DateTime fecha { get; set; }
        public String ubicacion { get; set; }
        public String tam_cant_dir { get; set; }
        public String tipoinfo { get; set; }
        public String resolucion { get; set; }
        public String sol_amp_tiempos { get; set; }
        public String art7 { get; set; }
        public String no_oficio { get; set; }
        public DateTime fecha_oficio { get; set; }
        public DateTime fecha_comite { get; set; }
    }
}
