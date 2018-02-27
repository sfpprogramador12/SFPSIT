using System;

namespace SFP.SIT.SERVICES.Model.Sol
{
    public class SolicitudGridMdl
    {
        public Int64 us_folio { get; set; }
        public DateTime us_fechasol { get; set; }
        public DateTime nre_fecini { get; set; }

        public Int32 carac1 { get; set; }
        public Int32 carac2 { get; set; }
        public Int32 carac3 { get; set; }
        public Int32 carac4 { get; set; }
        public Int32 carac5 { get; set; }
        public Int32 carac6 { get; set; }

        public Int32 seg_diassemaforo { get; set; }
        public Int32 seg_colorsemaforo { get; set; }
        public String us_des { get; set; }
        public String us_dat { get; set; }

        public String tso_descripcion { get; set; }
        public String men_descripcion { get; set; }        
        public String krt_rubro { get; set; }

        public SolicitudGridMdl() { }

    }
}
