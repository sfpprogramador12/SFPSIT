using SFP.SIT.AFD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFP.SIT.WEB.Models
{
    public class EdoActViewModel
    {
        /* FORMA NODO A LLAMAR*/
        public String nodoView { set; get; }

        /* VALORES DE IDENTIFICACION */
        public String titulo { set; get; }
        public Int32 tipoPrcActual { set; get; }
        public Int64 folio { set; get; }
        public String solFecIni { set; get; }

        public Int64 solFecIniTicks { set; get; }
        public Int32 solTipo { set; get; }
        public Int64 nodClave { set; get; }
        public Int32 araClave { set; get; }
        public Int32 perClave { set; get; }
        public String fecIni { set; get; }
        public String fecAct { set; get; }

        public String turnarArea { set; get; }
        public String turnarTransp{ set; get; }
        public String turnarAreasInternas { set; get; }
        public String controlName { set; get; }
        public String actionName { set; get; }

        /* VALORES AUXILIARES */
        public String tipoAristaInfomex { set; get; }
        public String observacion { set; get; }
        public String subclasificar { set; get; }
        public String tipoComiteRubro { set; get; }

        /* VALORES PANEL IZQUIERDO */
        public String solicitante { set; get; }
        public String solicitud { set; get; }
        public String aclaracion { set; get; }
        public String recRevision { set; get; }
        public Int16 fecAclaracion { set; get; }
        
        /* LISTA DE ACCIONES */
        public List<AfdEdoPdoMdl> lstEdoAccion { set; get; }

        public string listaAccion { set; get; }
    /* CATALOGOS */
    public String tipoModoEntrega { set; get; }
        
        /* CLASES AUXILIARES */
        public RespNotificarViewModel respNotificarVM { set; get; }

        /* VALORES AUXILIARES */
        public String etiqueta { set; get; }
        public int accion { set; get; }

        public Char btnAccion { set; get; }

        /* RESPUESTAS DE LAS ÁREAS */
        public String respAreas { set; get; }

        /* RESPUESTA FINAL */
        public String respuesta { set; get; }
        public String respuestaContenido { set; get; }
        public Int64 respuestaDoc { set; get; }
        public String respuestaDocNombre { set; get; }



        /* RESPUESTAS PARCIALES*/
        public String GridResp { set; get; }

        public String Datos()
        {
            return "{ EdoAct = { \"titulo\" :\"" + titulo + "\", \"tipoPrcActual\":" + tipoPrcActual + ", \"folio\":" + folio +
                ", \"solFecIni\":\"" + solFecIni + "\" ,\"solTipo\":" + solTipo + " ,\"claNodo\":" + nodClave +
                " ,\"fecIni\":\"" + fecIni  + "\" ,\"fecAct\":\"" + fecAct + "\" ,\"turnarArea\":" + turnarArea +
                " ,\"controlName\":\"" + controlName + "\" ,\"actionName\":\"" + actionName + "\"} }";
        }
    }
}
