using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SERVICES.Model.Imp
{
    public class ImportarSolicitudMdl
    {
        public Int64 us_clafolio { get; set; }
        public string us_folio { get; set; }
        public DateTime us_fechasol { get; set; }
        public int kue_claenl { get; set; }
        public string us_otromod { get; set; }
        public string us_des { get; set; }
        public string us_dat { get; set; }
        public string us_arcdes { get; set; }
        public DateTime us_fecrec { get; set; }
        public int idmedioentrada { get; set; }
        public DateTime us_fecent { get; set; }
        public DateTime us_fecenv { get; set; }
        public DateTime us_fecharesp { get; set; }
        public int idrespuesta { get; set; }
        public int idtiposol { get; set; }
        public string us_otroderechoacceso { get; set; }
        public string motivodesecha { get; set; }
        public int notificado { get; set; }
        public string us_usuario { get; set; }
        public int idformaentrega { get; set; }
        public int km_clamod { get; set; }



        public int idtipo { get; set; }        
        public string us_repleg { get; set; }
        public string us_rfc { get; set; }
        public string us_apepat { get; set; }
        public string us_apemat { get; set; }
        public string us_nombre { get; set; }
        public string us_curp { get; set; }
        public string us_calle { get; set; }
        public string us_numext { get; set; }
        public string us_numint { get; set; }
        public string us_col { get; set; }
        public string us_codpos { get; set; }
        public string us_tel { get; set; }
        public string us_corele { get; set; }
        public string us_edoext { get; set; }
        public string us_ciudadext { get; set; }
        public string us_sexo { get; set; }
        public DateTime us_fecnac { get; set; }                                
        public string us_otraocupacion { get; set; }
        public string us_otroniveleduc { get; set; }                         
        public string nivedu_clave { get; set; }
        public int kpa_clapai { get; set; }
        public int ke_claest { get; set; }
        public int kmu_clamun { get; set; }
        public int ko_claocup { get; set; }
        
       
        public ImportarSolicitudMdl() { }
    }
}
