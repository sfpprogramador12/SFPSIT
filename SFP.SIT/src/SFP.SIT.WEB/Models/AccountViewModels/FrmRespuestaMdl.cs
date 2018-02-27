using System;


namespace SFP.SIT.WEB.Models
{
    public class FrmRespuestaMdl
    {
        public Int64 folio { get; set; }
        public int claArista { get; set; }
        public int tipoArista { get; set; }
        public string art7 { get; set; }
        public int tipoInfo { get; set; }
        public int modoEntrega { get; set; }
        public string tamCantDir { get; set; }
        public string ubicacion { get; set; }
        public string resolucion { get; set; }        
    }

}
