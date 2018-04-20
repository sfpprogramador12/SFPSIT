using SFP.Persistencia.Model;
using SFP.SIT.SERV.Model.DOC;
using SFP.SIT.SERV.Model.RED;
using SFP.SIT.SERV.Model.RESP;
using SFP.SIT.SERV.Model.SNT;
using SFP.SIT.SERV.Model.SOL;
using System;
using System.Collections.Generic;

namespace SFP.SIT.AFD.Model
{
    public class AfdNodoFlujo : SIT_RED_NODOESTADO
    {
        public AfdNodoFlujo(int nodclave, string nodDescripcion, string nedurl, int nedtipo)
            : base(nedtipo, nedurl, nodDescripcion, nodclave)
        {
        }
        public string clase { get; set; }
        public Dictionary<int, int> dicAccionEstado { get; set; }
        public Dictionary<int, AfdEdoPdoMdl> DicAristaPlazo { get; set; }        
    }


    public class AfdEdoPdoMdl
    {
        public int id { get; set; }
        public string text { get; set; }
        public int estadoFinal { get; set; }
        public string forma { get; set; }
        public int nivel { get; set; }
        public string formato { get; set; }
        public string fecLimite { get; set; }
        public int plazo { get; set; }
    }

    public class AfdEdoPdoUIMdl
    {
        public int aristaID { get; set; }
        public int aristaDesc { get; set; }
        public int plazo { get; set; }
    }

    public class AfdEdoDataMdl
    {
        // SOLICIUTD 
        public Int64 solClave { set; get; }
        public int ID_AreaInai { set; get; }
        public int ID_AreaUT { set; get; }
        public int ID_AreaUTN { set; get; }
        public int ID_AreaCT { set; get; }
        public int ID_AreaDestino { set; get; }        
        public int ID_AreaOrigen { set; get; }
        public int ID_AreaOrigenPerfil { set; get; }
        public long ID_ClaArista { set; get; }
        public string ID_Clasificar { set; get; }
        public long ID_ClaAristaActual { set; get; }
        public int ID_ClaAfd { set; get; }
        public int ID_EstadoActual { set; get; }
        public int ID_Capa { set; get; }
        public int ID_Hito { set; get; }
        public Int32 ID_EstadoSiguiente { set; get; }
        public Int32 ID_EstadoMensaje { set; get; }
        public DateTime ID_FecEstimada {set; get; }

        public int ID_PerfilDestino { set; get; }


        // USUARIO - ORIGEN - DESTINO - AUSENCIA
        public int usrClaveOrigen { set; get; }
        public int usrClaveDestino { set; get; }
        public int usrClaveAusencia { set; get; }


        // RESPUESTAS
        public long repClave { set; get; }
        public Int32 rtpclave { set; get; }

        // CONJUNTO DE RESPUESTAS
        public SIT_RESP_RESPUESTA oRespRespuesta { set; get; }
        public int repEdoActual { set; get; }
             
        public String Observacion { set; get; }
        public DateTime FechaRecepcion { set; get; }
        public List<SIT_SOL_PROCESOPLAZOS > lstProcesoPlazos { set; get; }
        //public List<Tuple<int, string,int>> lstPersonasTurnar{ set; get; }
        public Dictionary<Int64, char> dicDiaNoLaboral { set; get; }
        ////////public Dictionary<int, int> dicPerfilArea { set; get; }
        public SIT_SNT_SOLICITANTE solicitante { set; get; }
        public SIT_SOL_SOLICITUD  solicitud { set; get; }
        public SIT_RESP_RREVISION  recRevisionMdl { set; get; }
        public SIT_RED_NODO AFDnodoOrigen { set; get; }
        public SIT_RED_NODO AFDnodoActMdl { set; get; }
        //////////public SIT_ARISTA_RESOLUCION  AFDresolucionMdl { set; get; }
        //////////public SIT_ARISTA_COMITE  AFDrescomiteMdl { set; get; }
        public SIT_SOL_SEGUIMIENTO  AFDseguimientoMdl { set; get; }        
        public Dictionary<Int32, AfdNodoFlujo> dicAfdFlujo { set; get; }

        public Dictionary<string, object> dicAuxRespuesta { set; get; }

        ////////public string Observacion;
        public CfgSharePointMdl SharePointCxn { set; get; }
        
        public List<DocContenidoMdl> lstDocContenidoMdl { set; get; }

        public String Datos()
        { 
            return "{ AfdEdoDataMdl = { \"folio\" :" + solClave + ", \"ClaArista\":" + ID_ClaArista + ", \"RespTipo\":" + rtpclave +
                ", \"EstadoActual\":" + ID_EstadoActual + ",\"ID_Capa\":" + ID_Capa + " ,\"ID_Hito\":" + ID_Hito +
                " ,\"ID_FecEstimada\":\""+ ID_FecEstimada.ToString("DD/MM/YYYYY") + "\" ,\"FechaRecepcion\":\"" + FechaRecepcion.ToString("DD/MM/YYYYY") + "\"} }";
        }
       


    }
}
