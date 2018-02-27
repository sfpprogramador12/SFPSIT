using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SERV.Model.IMP
{
    public class ImpInfomexArchivoSolMdl : ImportarSolicitudMdl
    {
        public int EstadoSolicitud { get; set; }
        public String UnidadEnlace { get; set; }
        public String TipoUsuario { get; set; }
        public String Pais { get; set; }
        public String EstidadFederativa { get; set; }
        public String Municipio { get; set; }
        public String Ocupacion { get; set; }
        public String ModoEntrega{ get; set; }
        public String Mensaje { get; set; }
        public int Error { get; set; }

        public ImpInfomexArchivoSolMdl() { }
        //public ImpInfomexArchivoSolMdl(
        //    Int64 solclave, string solclave, int idtipo, DateTime solfecrec, string sntrepleg,
        //    string sntrfc, string sntapepat, string sntapemat, string sntnombre, string sntcurp,
        //    string sntcalle, string sntnumext, string sntnumint, string sntcol, string sntcodpos,
        //    string snttel, string sntcorele, string sntedoext, string sntciudadext, string sntsexo,
        //    DateTime sntfecnac, string solotromod, string solarcdes, string soldes, string soldat,
        //    string sntusuario, DateTime solfecsol, int us_status, int us_acuse, DateTime solfecent,
        //    string us_folint, int us_acuseuni, string us_noguia, DateTime solfecenv, DateTime solfecresp,
        //    string sntotraocup, string sntotroniveledu, string                         afdDataMdl.solicitud.solotroderacc = regArchivoSol.us_otroderechoacceso;, string us_estadoasignacion, int metclave,
        //    int idsemaforo, int solrespclave, string quienenvio, int idtiposol, int idformaentrega,
        //    int colclave, int numpreg, int clasubcat, string sntniveduc, int deracce_clave,
        //    string solmotdesecha, int solnotificado, int clasubtem, string auth_acuse, int paiclave,
        //    int edoclave, int munclave, int ocuClave, int km_clamod, int kue_claenl,

        //    int EstadoSolicitud, String UnidadEnlace, String TipoUsuario, String Pais, String EstidadFederativa, 
        //    String Municipio, String Ocupacion, String ModoEntrega
        //    ) : base(
        //        solclave,    solclave,       idtipo,     solfecrec,      sntrepleg,
        //        sntrfc,         sntapepat,      sntapemat,  sntnombre,      sntcurp,
        //        sntcalle,       sntnumext,      sntnumint,  sntcol,         sntcodpos,
        //        snttel,         sntcorele,      sntedoext,  sntciudadext,   sntsexo,
        //        sntfecnac,      solotromod,     solarcdes,  soldes,         soldat,
        //        sntusuario,     solfecsol,    us_status,  us_acuse,       solfecent,
        //        us_folint,      us_acuseuni,    us_noguia,  solfecenv,      solfecresp,
        //        sntotraocup, sntotroniveledu,                         afdDataMdl.solicitud.solotroderacc = regArchivoSol.us_otroderechoacceso;, us_estadoasignacion, metclave,
        //        idsemaforo,     solrespclave,    quienenvio, idtiposol,      idformaentrega,
        //        colclave,       numpreg,        clasubcat,  sntniveduc,   deracce_clave,
        //        solmotdesecha,  solnotificado,     clasubtem,  auth_acuse,     paiclave,
        //        edoclave,      munclave,     ocuClave, km_clamod,      kue_claenl
        //    )
        //{
        //    this.EstadoSolicitud = EstadoSolicitud;
        //    this.UnidadEnlace = UnidadEnlace;
        //    this.TipoUsuario = TipoUsuario;
        //    this.Pais = Pais;
        //    this.EstidadFederativa = EstidadFederativa;
        //    this.Municipio = Municipio;
        //    this.Ocupacion = Ocupacion;
        //    this.ModoEntrega = ModoEntrega;
        //}
    }
}

