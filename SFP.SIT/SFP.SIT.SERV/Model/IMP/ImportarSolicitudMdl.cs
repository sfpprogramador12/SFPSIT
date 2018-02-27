using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SERV.Model.IMP
{
    public class ImportarSolicitudMdl
    {
        public Int64 solclave { get; set; }
        public string solfolio { get; set; }
        public DateTime solfecsol { get; set; }
        public int kue_claenl { get; set; }
        public string solotromod { get; set; }
        public string soldes { get; set; }
        public string soldat { get; set; }
        public string solarcdes { get; set; }
        public DateTime solfecrec { get; set; }
        public int metclave { get; set; }
        public DateTime solfecent { get; set; }
        public DateTime solfecenv { get; set; }
        public DateTime solfecresp { get; set; }
        public int solrespclave { get; set; }
        public int idtiposol { get; set; }
        public string us_otroderechoacceso { get; set; }
        public string solmotdesecha { get; set; }
        public int solnotificado { get; set; }
        public string sntusuario { get; set; }
        public int idformaentrega { get; set; }
        public int km_clamod { get; set; }



        public int idtipo { get; set; }        
        public string sntrepleg { get; set; }
        public string sntrfc { get; set; }
        public string sntapepat { get; set; }
        public string sntapemat { get; set; }
        public string sntnombre { get; set; }
        public string sntcurp { get; set; }
        public string sntcalle { get; set; }
        public string sntnumext { get; set; }
        public string sntnumint { get; set; }
        public string sntcol { get; set; }
        public string sntcodpos { get; set; }
        public string snttel { get; set; }
        public string sntcorele { get; set; }
        public string sntedoext { get; set; }
        public string sntciudadext { get; set; }
        public string sntsexo { get; set; }
        public DateTime sntfecnac { get; set; }                                
        public string sntotraocup { get; set; }
        public string sntotroniveledu { get; set; }                         
        public string sntniveduc { get; set; }
        public int paiclave { get; set; }
        public int edoclave { get; set; }
        public int munclave { get; set; }
        public int ocuClave { get; set; }
        
       
        public ImportarSolicitudMdl() { }
    }
}
