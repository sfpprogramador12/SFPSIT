using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SERVICES.Model.Imp
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
        //    Int64 us_clafolio, string us_folio, int idtipo, DateTime us_fecrec, string us_repleg,
        //    string us_rfc, string us_apepat, string us_apemat, string us_nombre, string us_curp,
        //    string us_calle, string us_numext, string us_numint, string us_col, string us_codpos,
        //    string us_tel, string us_corele, string us_edoext, string us_ciudadext, string us_sexo,
        //    DateTime us_fecnac, string us_otromod, string us_arcdes, string us_des, string us_dat,
        //    string us_usuario, DateTime us_fechasol, int us_status, int us_acuse, DateTime us_fecent,
        //    string us_folint, int us_acuseuni, string us_noguia, DateTime us_fecenv, DateTime us_fecharesp,
        //    string us_otraocupacion, string us_otroniveleduc, string us_otroderechoacceso, string us_estadoasignacion, int idmedioentrada,
        //    int idsemaforo, int idrespuesta, string quienenvio, int idtiposol, int idformaentrega,
        //    int colclave, int numpreg, int clasubcat, string nivedu_clave, int deracce_clave,
        //    string motivodesecha, int notificado, int clasubtem, string auth_acuse, int kpa_clapai,
        //    int ke_claest, int kmu_clamun, int ko_claocup, int km_clamod, int kue_claenl,

        //    int EstadoSolicitud, String UnidadEnlace, String TipoUsuario, String Pais, String EstidadFederativa, 
        //    String Municipio, String Ocupacion, String ModoEntrega
        //    ) : base(
        //        us_clafolio,    us_folio,       idtipo,     us_fecrec,      us_repleg,
        //        us_rfc,         us_apepat,      us_apemat,  us_nombre,      us_curp,
        //        us_calle,       us_numext,      us_numint,  us_col,         us_codpos,
        //        us_tel,         us_corele,      us_edoext,  us_ciudadext,   us_sexo,
        //        us_fecnac,      us_otromod,     us_arcdes,  us_des,         us_dat,
        //        us_usuario,     us_fechasol,    us_status,  us_acuse,       us_fecent,
        //        us_folint,      us_acuseuni,    us_noguia,  us_fecenv,      us_fecharesp,
        //        us_otraocupacion, us_otroniveleduc, us_otroderechoacceso, us_estadoasignacion, idmedioentrada,
        //        idsemaforo,     idrespuesta,    quienenvio, idtiposol,      idformaentrega,
        //        colclave,       numpreg,        clasubcat,  nivedu_clave,   deracce_clave,
        //        motivodesecha,  notificado,     clasubtem,  auth_acuse,     kpa_clapai,
        //        ke_claest,      kmu_clamun,     ko_claocup, km_clamod,      kue_claenl
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

