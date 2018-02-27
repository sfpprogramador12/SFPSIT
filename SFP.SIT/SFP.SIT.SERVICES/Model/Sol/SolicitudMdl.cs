using System;

namespace SFP.SIT.SERVICES.Model.Sol
{
    public class SolicitudMdl
    {
        public Int64 us_clafolio { get; set; }
        public String us_folio { get; set; }
        public DateTime us_fechasol { get; set; }
        public int us_unienl { get; set; }
        public String us_otromod { get; set; }
        public String us_des { get; set; }
        public String us_dat { get; set; }
        public String us_arcdes { get; set; }
        public DateTime us_fecrec { get; set; }
        public int idmedioentrada { get; set; }
        public DateTime us_fecent { get; set; }
        public DateTime us_fecenv { get; set; }
        public DateTime us_fecharesp { get; set; }
        public int idrespuesta { get; set; }
        public int tso_clatiposol { get; set; }
        public String us_otroderechoacceso { get; set; }
        public String motivodesecha { get; set; }
        public int notificado { get; set; }
        public long us_clausu { get; set; }
        public int idformaentrega { get; set; }
        public int us_modent { get; set; }
        public int krt_clatema { get; set; }
        public DateTime us_fecaclaracion { get; set; }
        public int krp_claproceso { get; set; }
        public int kpz_tipoplazo { get; set; }
        public DateTime us_fecrecursorevision { get; set; }

                       
        public SolicitudMdl() { }

        public SolicitudMdl(
            long us_clafolio,   String us_folio,    DateTime us_fecrec, String us_otromod,
            String us_arcdes,   String us_des,      String us_dat,      DateTime us_fechasol,
            DateTime us_fecent, DateTime us_fecenv, DateTime us_fecharesp,
            String us_otroderechoacceso,  int idmedioentrada, 
            int idrespuesta,    String motivodesecha, int notificado,
            int idtiposol,      long us_clausu,
            int us_unienl,      int idformaentrega, int us_modent,      int krt_clatema,    
            DateTime us_fecaclaracion,  int krp_claproceso,     DateTime us_fecrecursorevision)
        {
            this.us_clafolio = us_clafolio;
            this.us_folio = us_folio;            
            this.us_fecrec = us_fecrec;
            this.us_otromod = us_otromod;
            this.us_arcdes = us_arcdes;
            this.us_des = us_des;
            this.us_dat = us_dat;
            this.us_fechasol = us_fechasol;
            this.us_fecent = us_fecent;
            this.us_fecenv = us_fecenv;
            this.us_fecharesp = us_fecharesp;
            this.us_otroderechoacceso = us_otroderechoacceso;
            this.idmedioentrada = idmedioentrada;
            this.idrespuesta = idrespuesta;
            this.motivodesecha = motivodesecha;
            this.notificado = notificado;
            this.tso_clatiposol = idtiposol;
            this.us_clausu = us_clausu;
            this.us_unienl = us_unienl;
            this.idformaentrega = idformaentrega;
            this.us_modent = us_modent;
            this.krt_clatema = krt_clatema;
            this.us_fecaclaracion = us_fecaclaracion;
            this.krp_claproceso = krp_claproceso;
            this.us_fecrecursorevision = us_fecrecursorevision;
        }
    }
}
