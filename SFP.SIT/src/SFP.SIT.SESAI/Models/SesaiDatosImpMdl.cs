using SFP.SIT.SERV.Model.ADM;
using SFP.SIT.SERV.Model.DOC;
using SFP.SIT.SERV.Model.RED;
using SFP.SIT.SERV.Model.RESP;
using SFP.SIT.SERV.Model.SNT;
using SFP.SIT.SERV.Model.SOL;
using System.Collections.Generic;

namespace SFP.SIT.SESAI.Models
{
    public class SesaiDatosImpMdl
    {
        public List<SIT_ADM_AREA> Areas { get; set; }
        public List<SIT_ADM_AREATIPO > AreasTipo { get; set; }
        public List<SIT_ADM_AREANIVEL> AreasNivel { get; set; }
        public List<SIT_ADM_AREAHIST> AreasHist { get; set; }
        public List<SIT_ADM_AREAGESTION> AreasGestion { get; set; }

        public List<SIT_ADM_PERFIL> Perfiles { get; set; }
        public List<SIT_ADM_USUARIO> Usuarios { get; set; }
        public List<SIT_ADM_USUARIOAREA> UsrArea { get; set; }
        public List<SIT_ADM_USRPERFIL> UserPerfil { get; set; }
        public List<SIT_ADM_MODULO> Modulo { get; set; }



        public List<SIT_ADM_PERFILMOD> PerfilMod { get; set; }
        public List<SIT_ADM_CLASES> Clases { get; set; }
        public List<SIT_ADM_PERFILCLASES> PerfilClases { get; set; }
        public List<SIT_RED_AFD> Afd { get; set; }


        /* RESPUESTAS */
        public List<SIT_RESP_TIPO> RespTipo { get; set; }
        public List<SIT_RESP_CLASINFO> RespClasInfo { get; set; }
        public List<SIT_RESP_TIPOINFO> RespTipoInfo { get; set; }
        public List<SIT_RESP_ESTADO> RespEstado { get; set; }
        ///////public List<SIT_RESP_CONTENIDOTIPO> RespContenidoTipo { get; set; }
        public List<SIT_RESP_MOMENTO> RespMomento { get; set; }
        public List<SIT_RESP_TEMA> RespTema { get; set; }
        public List<SIT_RESP_REPRODUCCION> RespReproduccion { get; set; }


        public List<SIT_RED_NODOESTADO> NodoEstado { get; set; }
        public List<SIT_ADM_PERFILNODO> PerfilNodo { get; set; }
        public List<SIT_RED_AFDFLUJO> AfdFlujo { get; set; }
        public List<SIT_SNT_PAIS> Pais { get; set; }
        public List<SIT_SNT_ESTADO> Estado { get; set; }



        public List<SIT_SNT_MUNICIPIO> Municipio { get; set; }
        public List<SIT_SOL_MEDIOENTRADA> MedioEntrada { get; set; }
        public List<SIT_SNT_OCUPACION> Ocupacion { get; set; }

        

        public List<SIT_SNT_SOLICITANTETIPO> TipoSolicitante { get; set; }

        public List<SIT_SOL_SOLICITUDTIPO> TipoSolicitud { get; set; }
        public List<SIT_SOL_PROCESO> ProcesoMod { get; set; }
        public List<SIT_SOL_PROCESOPLAZOS> ProcesoPlazosMod { get; set; }
        public List<SIT_SOL_MODOENTREGA> SolModoEntrega { get; set; }
        public List<SIT_ADM_DIANOLABORAL> DiaNoLaboral { get; set; }



        public List<SIT_DOC_EXTENSION> TipoExtension { get; set; }
        public List<SIT_RESP_COMITERUBRO> ComiteRubro { get; set; }        
        public List<SIT_ADM_CONFIGURACION> Configuracion { get; set; }
                
    }
}
