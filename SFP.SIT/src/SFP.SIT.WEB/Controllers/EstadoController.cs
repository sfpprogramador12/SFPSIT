using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFP.SIT.WEB.Services;
using SFP.SIT.WEB.Injection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using SFP.SIT.WEB.Util;
using SFP.SIT.WEB.Models;
using SFP.SIT.AFD.Model;
using SFP.SIT.SERV.Dao;
using SFP.SIT.SERV.Util;
using SFP.SIT.SERV.Negocio;
using SFP.SIT.SERV.Dao.SOL;
using SFP.SIT.SERV.Dao.SNT;
using SFP.SIT.SERV.Model.SOL;
using SFP.SIT.AFD.Core;
using SFP.SIT.SERV.Dao.RED;
using SFP.SIT.SERV.Model.RED;
using System.Data;
using SFP.SIT.SERV.Dao.RESP;
using SFP.SIT.SERV.Model.DOC;
using SFP.SIT.SERV.Model.RESP;
using SFP.SIT.SERV.Model._Consultas;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SFP.SIT.WEB.Controllers
{
    public class EstadoController : SitBaseCtlr
    {
        SolicitudSer _solServ;

        public EstadoController(ICacheWebSIT memCache, IHttpContextAccessor httpContextAccessor, ILogger<EstadoController> logger, IHostingEnvironment app)
            : base(memCache, httpContextAccessor, logger, app)
        {
            _iUsuario = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ConstantesWeb.Usuario.CLAVE).Value);
            _solServ = new SolicitudSer(_sitDmlDbSer);
        }


        private string DelimitarPlazo(AfdEdoPdoMdl edoAccionMdl, DateTime solFecha, ref CalcularPlazoNeg calcularPlazoNeg)
        {           
            DateTime dtFechaLimite = calcularPlazoNeg.ObtenerFechaFinal(solFecha, edoAccionMdl.plazo);
            //dtFechaLimite.ToString("yyyy/MM/dd");
            return "2018/12/31";
        }

        public EdoActViewModel ObtenerDatosGral(long nodo, bool bSolicitante, int iPrc, int iArea, int iPerfil)
        {
            EdoActViewModel edoActVM = new EdoActViewModel();
            DateTime dteFecAclaracion = new DateTime();
            

            SIT_RED_NODO redNodo = _sitDmlDbSer.operEjecutar<SIT_RED_NODODao>(nameof(SIT_RED_NODODao.dmlSelectNodoID), nodo) as SIT_RED_NODO;
            CalcularPlazoNeg calcularPlazoNeg = new CalcularPlazoNeg(_memCacheSIT.ObtenerDato(CacheWebSIT.DIC_DIA_NO_LABORAL) as Dictionary<Int64, char>);
            SIT_SOL_SOLICITUD solSolicitud = _sitDmlDbSer.operEjecutar<SIT_SOL_SOLICITUDDao>(nameof(SIT_SOL_SOLICITUDDao.dmlSelectID),
                new SIT_SOL_SOLICITUD { solclave = (long)redNodo.solclave }) as SIT_SOL_SOLICITUD;

            edoActVM.folio = solSolicitud.solclave;
            edoActVM.solFecIni = solSolicitud.solfecrec.ToString("dd/MM/yyyy");
            edoActVM.solFecIniTicks = solSolicitud.solfecsol.Ticks;
            edoActVM.solTipo = (int)solSolicitud.sotclave;
            edoActVM.nodClave = nodo;
            edoActVM.perClave = iPerfil;
            edoActVM.fecIni = "new Date('" + solSolicitud.solfecrec.ToString("yyyy/MM/dd") + "')";
            edoActVM.fecAct = "new Date('" + DateTime.Now.ToString("yyyy/MM/dd") + "')";
            edoActVM.araClave = (int)redNodo.araclave;

            /* INICIO VALORES PANEL IZQUIERDO     */
            // SOLICITANTE
            if (bSolicitante)
                edoActVM.solicitante = JsonTransform.convertJson(
                    _sitDmlDbSer.operEjecutar<SIT_SNT_SOLICITANTEDao>(nameof(SIT_SNT_SOLICITANTEDao.dmlSelectSolicitanteTranspuestoPorID), solSolicitud.solclave) as DataTable);
            else
                edoActVM.solicitante = JsonTransform.NO_RECORDS;

            //SOLICTUD
            edoActVM.solicitud = _solServ.ObtenerSolicitudTranspuesta(solSolicitud.solclave, out dteFecAclaracion);

            // ACLARACION
            SIT_SOL_SEGUIMIENTO segAclMdl = _solServ.ObtenerSeguimiento(edoActVM.folio, Constantes.ProcesoTipo.ACLARACION);
            if (dteFecAclaracion != DateTime.MinValue)
                edoActVM.aclaracion = _solServ.ObtenerAclaracion(edoActVM.folio,
                    Constantes.Respuesta.REQ_INFO_ADICIONAL, Constantes.Respuesta.RECEPCION_INFO_ADICIONAL);
            else
                edoActVM.aclaracion = JsonTransform.NO_RECORDS;

            // RECURSO DE REVISION
            edoActVM.recRevision = _solServ.ObtenerRecRevision(solSolicitud.solclave);
            /* FIN VALORES PANEL IZQUIERDO     */

            /* ACTUALIZAR LA LISTA DE ACCIONES  */
            Dictionary<Int32, AfdNodoFlujo> dicFlujoTrabajo = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO) as Dictionary<Int32, AfdNodoFlujo>;
            AfdNodoFlujo NodoFlujo = dicFlujoTrabajo[(int)redNodo.nedclave];

            List<AfdEdoPdoMdl> lstEdoAccion = new List<AfdEdoPdoMdl>();
            foreach (KeyValuePair<int, AfdEdoPdoMdl> pair in NodoFlujo.DicAristaPlazo)
                lstEdoAccion.Add(pair.Value);

            AfdEdoPdoMdl edoQuitarTurnar = null;
            AfdEdoPdoMdl edoQuitarAmpliacion = null;

            edoActVM.turnarTransp = JsonTransform.NO_RECORDS;
            edoActVM.turnarArea = JsonTransform.NO_RECORDS;
            edoActVM.turnarAreasInternas = JsonTransform.NO_RECORDS;

            foreach (AfdEdoPdoMdl edoAccionMdl in lstEdoAccion)
            {
                if (edoAccionMdl.plazo > 0)
                {
                    edoAccionMdl.fecLimite = DelimitarPlazo(edoAccionMdl, solSolicitud.solfecrec, ref calcularPlazoNeg);
                }


                if (edoAccionMdl.id == Constantes.Respuesta.ASIGNAR)
                {
                    Dictionary<string, object> dicParam = new Dictionary<string, object>();
                    dicParam.Add(Constantes.Parametro.FECHA, DateTime.Now);
                    dicParam.Add(DButil.SIT_ADM_PERFIL_COL.PERCLAVE, Constantes.Perfil.PRUT);

                    edoActVM.turnarTransp = JsonTransform.convertJson(_sitDmlDbSer.operEjecutar<ConsultaDao>(nameof(ConsultaDao.dmlSelectPersonasPerfil), dicParam));

                    if (edoActVM.turnarTransp == JsonTransform.NO_RECORDS)
                        edoQuitarTurnar = edoAccionMdl;
                }
                else if (edoAccionMdl.id == Constantes.Respuesta.TURNAR)
                {
                    Dictionary<string, object> dicParam = new Dictionary<string, object>();
                    dicParam.Add(Constantes.Parametro.FECHA, DateTime.Now);
                    dicParam.Add(DButil.SIT_ADM_AREAHIST_COL.ARHREPORTA, iArea);

                    if (iPerfil == Constantes.Perfil.UA)
                    {
                        edoActVM.turnarAreasInternas = JsonTransform.convertJson(_sitDmlDbSer.operEjecutar<ConsultaDao>(nameof(ConsultaDao.dmlSelectAreasActualReporta), dicParam));
                        if (edoActVM.turnarAreasInternas == JsonTransform.NO_RECORDS)
                            edoQuitarTurnar = edoAccionMdl;
                    }
                    else
                    {
                        edoActVM.turnarArea = JsonTransform.convertJson(_sitDmlDbSer.operEjecutar<ConsultaDao>(nameof(ConsultaDao.dmlSelectAreasActual), DateTime.Now));
                        if (edoActVM.turnarArea == JsonTransform.NO_RECORDS)
                            edoQuitarTurnar = edoAccionMdl;
                    }
                }

                else if (edoAccionMdl.id == Constantes.Respuesta.AMPLIACION_PLAZO)
                {
                    if (segAclMdl != null)
                        edoQuitarAmpliacion = edoAccionMdl;
                }

                else if (edoAccionMdl.id == Constantes.Respuesta.RESPUESTA_MULTIPLE)
                {
                    edoActVM.lstEdoSubAccion = new List<AfdEdoPdoMdl>();
                    List<SIT_RESP_TIPO> lstResptipo = _sitDmlDbSer.operEjecutar<SIT_RESP_TIPODao>(nameof(SIT_RESP_TIPODao.dmlSelectRespTipoLst), 6) as List<SIT_RESP_TIPO>;
                    for (int iIdxResp = 0; iIdxResp < lstResptipo.Count; iIdxResp++)
                    {
                        AfdEdoPdoMdl edoSubAccionMdl = new AfdEdoPdoMdl
                        {
                            id = lstResptipo[iIdxResp].rtpclave,
                            plazo = lstResptipo[iIdxResp].rtpplazo,
                            fecLimite = "",
                            forma = lstResptipo[iIdxResp].rtpforma,
                            nivel = lstResptipo[iIdxResp].rtptipo,
                            formato = lstResptipo[iIdxResp].rtpformato,
                            text = lstResptipo[iIdxResp].rtpdescripcion
                        };

                        if (lstResptipo[iIdxResp].rtpplazo > 0)
                        {
                            edoSubAccionMdl.fecLimite = DelimitarPlazo(edoSubAccionMdl, solSolicitud.solfecrec, ref calcularPlazoNeg);
                        }
                        edoActVM.lstEdoSubAccion.Add(edoSubAccionMdl);
                    }
                }
            }

            if (edoQuitarTurnar != null)
                lstEdoAccion.Remove(edoQuitarTurnar);

            if (edoQuitarAmpliacion != null)
                lstEdoAccion.Remove(edoQuitarAmpliacion);

            lstEdoAccion.Sort(delegate (AfdEdoPdoMdl x, AfdEdoPdoMdl y)
            {
                if (x.text == null && y.text == null) return 0;
                else if (x.text == null) return -1;
                else if (y.text == null) return 1;
                else return x.text.CompareTo(y.text);
            });
            edoActVM.lstEdoAccion = lstEdoAccion;
            edoActVM.listaAccion = JsonTransform.convertJson(lstEdoAccion);
            edoActVM.listaSubAccion = JsonTransform.convertJson(edoActVM.lstEdoSubAccion);
                

            edoActVM.controlName = "Flujo" + _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO).ToString();
            edoActVM.tipoPrcActual = iPrc;
            

            // AQUI COLOCO EL TITULO Y SU BUSQUEDA....
            String sProceso = _memCacheSIT.BuscarLista(CacheWebSIT.LST_SOL_PROCESO, iPrc.ToString());
            String sSolTipo = _memCacheSIT.BuscarLista(CacheWebSIT.LST_SOL_TIPO, solSolicitud.sotclave.ToString());
            String sPerfil = _memCacheSIT.BuscarLista(CacheWebSIT.LST_PERFIL, iPerfil.ToString());
            String sNodoEstado = _memCacheSIT.BuscarDiccionario(CacheWebSIT.DIC_NODO_ESTADO, (int)redNodo.nedclave);

            edoActVM.titulo = sPerfil.ToUpper() + " - " + sProceso + " - " + sSolTipo + " - " + sNodoEstado.ToUpper();

            return edoActVM;
        }

        /*********************************************
               UNIDAD  TRANSPARENCIA 
        ******************************************** */
        [HttpGet]
        public IActionResult UTrecibirSol(int iPrc, Int64 lNodo, int iArea, int iPerfil)
        {
            EdoActViewModel edoActVM = ObtenerDatosGral(lNodo, true, iPrc, iArea, iPerfil);
            edoActVM.nodoView = "UTrecibirSol";

            ViewBag.DatosGrals = edoActVM;
            ViewBag.ListaAccion = JsonTransform.convertJson(edoActVM.lstEdoAccion);

            _appLog.opdesc = "UTrecibirSol ";
            _appLog.data = "";
            return View();
        }

        /*********************************************
               PRUD 
        ******************************************** */
        [HttpGet]
        public IActionResult PRUDrecibirSol(int iPrc, Int64 lNodo, int iArea, int iPerfil, int iUsr)
        {
            EdoActViewModel edoActVM = ObtenerDatosGral(lNodo, true, iPrc, iArea, iPerfil);
            edoActVM.nodoView = "PRUDrecibirSol";

            ViewBag.DatosGrals = edoActVM;
            ViewBag.ListaAccion = JsonTransform.convertJson(edoActVM.lstEdoAccion);

            _appLog.opdesc = "PRUDrecibirSol ";
            _appLog.data = "";

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(DButil.SIT_RED_NODO_COL.NODCLAVE, lNodo);
            dicParam.Add(DButil.SIT_RED_NODORESP_COL.SDOCLAVE, Constantes.RespuestaEstado.ANALIZAR);

            // EL USUAIRO FIMRADO AUNQUE SE REQUIERE EN QUE ESTA SELECCIONADO..
            dicParam.Add(DButil.SIT_RESP_TURNAR_COL.USRCLAVE, _iUsuario);

            ViewBag.Instruccion = _sitDmlDbSer.operEjecutar<ConsultaDao>(nameof(ConsultaDao.dmlSelectInstrucciones), dicParam) as string;

            return View();
        }

        [HttpGet]
        public IActionResult PRUDasignar(int iPrc, Int64 lNodo, int iArea, int iPerfil, int iUsr)
        {
            EdoActViewModel edoActVM = ObtenerDatosGral(lNodo, true, iPrc, iArea, iPerfil);
            edoActVM.nodoView = "PRUDrecibirSol";

            ViewBag.DatosGrals = edoActVM;
            ViewBag.ListaAccion = JsonTransform.convertJson(edoActVM.lstEdoAccion);

            _appLog.opdesc = "PRUDrecibirSol ";
            _appLog.data = "";

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(DButil.SIT_RED_NODO_COL.NODCLAVE, lNodo);
            dicParam.Add(DButil.SIT_RED_NODORESP_COL.SDOCLAVE, Constantes.RespuestaEstado.ANALIZAR);

            // EL USUAIRO FIMRADO AUNQUE SE REQUIERE EN QUE ESTA SELECCIONADO..
            dicParam.Add(DButil.SIT_RESP_TURNAR_COL.USRCLAVE, _iUsuario);

            ViewBag.Instruccion = _sitDmlDbSer.operEjecutar<ConsultaDao>(nameof(ConsultaDao.dmlSelectInstrucciones), dicParam) as string;

            return View();
        }

        [HttpGet]
        public IActionResult PRUDrevisarRespSol(int iPrc, Int64 lNodo, int iArea, int iPerfil, int iUsr)
        {
            EdoActViewModel edoActVM = ObtenerDatosGral(lNodo, true, iPrc, iArea, iPerfil);
            edoActVM.nodoView = "PRUDrevisarResp";

            ViewBag.DatosGrals = edoActVM;
            
            List<NodoRespDetalle> lstUsrResp = _sitDmlDbSer.operEjecutar<ConsultaDao>(nameof(ConsultaDao.dmlSelectRespNodoAnterior), lNodo) as List<NodoRespDetalle>;
            ViewBag.DatosRespDetalle = JsonTransform.convertJson(lstUsrResp);

            // genero el json
            // locarog a la a fomra
            // selecicono y trae los datos..

            return View();
        }


        [HttpPost]
        public IActionResult PRUDAceptarRespSol(int repclave, Int64 nodClave)
        {
            SIT_RED_NODORESP nodoResp = new SIT_RED_NODORESP();
            nodoResp.nodclave = nodClave;
            nodoResp.repclave = repclave;
            nodoResp.sdoclave = Constantes.RespuestaEstado.ACEPTADA;

            String sDatos = _sitDmlDbSer.operEjecutar<SIT_RED_NODORESPDao>(nameof(SIT_RED_NODORESPDao.dmlEditar), nodoResp).ToString();
            return Content(sDatos);
        }


        [HttpPost]
        public IActionResult PRUDCorregirRespSol(Int64 solclaveCorregir, Int64 repclaveCorregir, Int64 nodClaveCorregir, string mensaje, Int64 nodOriClaveCorregir)
        {
            // Obtener los datos del origen... nodOriClaveCorregir
            SIT_RED_NODO oRedNodoOrigen = _sitDmlDbSer.operEjecutar<SIT_RED_NODODao>(nameof(SIT_RED_NODODao.dmlSelectNodoID), nodOriClaveCorregir) as SIT_RED_NODO;


            AfdEdoDataMdl afdDataMdl = NodoActualIni(solclaveCorregir, Constantes.Respuesta.ASIGNAR, nodClaveCorregir, (int) oRedNodoOrigen.usrclave, _iUsuario, _iUsuario, Constantes.Perfil.UA);
            afdDataMdl.ID_AreaDestino = (int) oRedNodoOrigen.araclave;
            afdDataMdl.rtpclave = Constantes.Respuesta.CORREGIR;

            //////////afdDataMdl.ID_EstadoActual


            // QEU AREA ES LE DESTINO_ VERIFICAR..
            // afdDataMdl.ID_AreaDestino = 

            SIT_RED_NODORESP nodoResp = new SIT_RED_NODORESP();
            nodoResp.nodclave = nodClaveCorregir;
            nodoResp.repclave = repclaveCorregir;
            nodoResp.sdoclave = Constantes.RespuestaEstado.CORREGIR;
            afdDataMdl.dicAuxRespuesta.Add(ProcesoGralDao.PARAM_RED_NODORESP, nodoResp);


            Dictionary<SIT_RESP_RESPUESTA, SIT_RESP_DETALLE> dicRespDetalle = new Dictionary<SIT_RESP_RESPUESTA, SIT_RESP_DETALLE>();

            SIT_RESP_RESPUESTA oRespuesta = new SIT_RESP_RESPUESTA
            {
                repcantidad = 0,
                megclave = 0,
                docclave = null,
                repoficio = null,
                repedofec = DateTime.Now,
                repclave = 0,
                rtpclave = Constantes.RespuestaEstado.CORREGIR
            };
            

            SIT_RESP_DETALLE oRespDetalle = new SIT_RESP_DETALLE
            {
                detclave = "CORREGIR",
                repclave = 0,
                docclave = null,
                detdescripcion = mensaje
            };
            dicRespDetalle.Add(oRespuesta, oRespDetalle);
            afdDataMdl.dicAuxRespuesta.Add(ProcesoGralDao.PARAM_DIC_RESP_DETALLE, dicRespDetalle);
            string oResultado = _sitDmlDbSer.operEjecutarTransaccion<AfdServicio>(nameof(AfdServicio.Accion), afdDataMdl).ToString();
            return Content("Agui debo de corregir");
        }

            
        /* ********************************************
                    UNIDAD ADMINISTRATIVA         
        ******************************************** */
        [HttpGet]
        public IActionResult UAanalizarSol(int iPrc, Int64 lNodo, int iArea, int iPerfil)
        {
            EdoActViewModel edoActVM = ObtenerDatosGral(lNodo, false, iPrc, iArea, iPerfil);
            edoActVM.nodoView = "UAanalizar";
            ViewBag.DatosGrals = edoActVM;

            _appLog.opdesc = "UAanalizar";
            _appLog.data = "  ";


            ViewBag.ListaAccion = edoActVM.listaAccion;
            ViewBag.ListaSubAccion = edoActVM.listaSubAccion;
            ViewBag.Nodo = lNodo;
            ViewBag.controlName = "Estado";


            //List<NodoRespuestaMdl> lstRespuesta = _sitDmlDbSer.operEjecutar<ConsultaDao>(nameof(ConsultaDao.dmlSelectRespuestaNodo), lNodo) as List<NodoRespuestaMdl>;
            //ViewBag.GridResp = JsonTransform.convertJson(lstRespuesta);

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(DButil.SIT_RED_NODO_COL.NODCLAVE, lNodo);
            dicParam.Add(DButil.SIT_RED_NODORESP_COL.SDOCLAVE, Constantes.RespuestaEstado.ANALIZAR);

            // EL USUAIRO FIMRADO AUNQUE SE REQUIERE EN QUE ESTA SELECCIONADO..
            dicParam.Add(DButil.SIT_RESP_TURNAR_COL.USRCLAVE, _iUsuario);
            ViewBag.Instruccion = _sitDmlDbSer.operEjecutar<ConsultaDao>(nameof(ConsultaDao.dmlSelectInstrucciones), dicParam) as string;

            return View();
        }


        /* ********************************************
            FUNCION GENERICA PARA CONTINUAR
        ******************************************** */


        [HttpPost]
        public IActionResult Responder(long solclaveR, int proclaveR, long nodclaveR, int araclaveR, int perclaveR, int rtpclaveR)
        {                    
            SIT_RED_NODORESP nodResp = new SIT_RED_NODORESP();
            nodResp.nodclave = nodclaveR;
            nodResp.sdoclave = Constantes.RespuestaEstado.PROPUESTA;
                                                                                                                     
            AfdEdoDataMdl afdDataMdl = NodoActualIni(solclaveR, rtpclaveR, nodclaveR, _iUsuario, _iUsuario, _iUsuario, perclaveR);             
            List<SIT_RESP_RESPUESTA> dmlSelectRespEdo = _sitDmlDbSer.operEjecutar<ConsultaDao>(nameof(ConsultaDao.dmlSelectRespEdo), nodResp) as List<SIT_RESP_RESPUESTA>;
            afdDataMdl.rtpclave = rtpclaveR;

            string oResultado = _sitDmlDbSer.operEjecutarTransaccion<AfdServicio>(nameof(AfdServicio.Accion), afdDataMdl).ToString();

            return RedirectToAction("BandejaEntrada", "Solicitud");
        }


        [HttpPost]
        public string UAanalizarBorrar(Int64 nodo, Int32 recid)
        {
            ////////////SIT_RED_NODORESP nodoResp = new SIT_RED_NODORESP( recid, nodo);            
            ////////////return _sitDmlDbSer.operEjecutarTransaccion<SIT_RED_NODORESPDao>(nameof(SIT_RED_NODORESPDao.dmlBorrar), nodoResp).ToString();
            return null;
        }


        /* ********************************************
                    D O C U M E N T O  S       
        ******************************************** */
        [HttpGet]
        public IActionResult Documento(Int32 id)
        {
            _appLog.opdesc = "Documento";
            _appLog.data = " { Documento : { \"id\":" + id + "}} ";

            DocContenidoMdl frmDocMdl = _solServ.ObtenerDocumentoID(id, _app.ContentRootPath + "\\" + ConstantesWeb.Carpetas.ARCHIVO);
            if (frmDocMdl != null)
                return File(frmDocMdl.doc_contenido, frmDocMdl.extmimetype, frmDocMdl.docnombre);
            else
                return Content("<b>El archivo a desplegar no fue ser localizado, favor de comunicarlo al área de Tecnologías de la Información</b>");

        }


        /* ********************************************
                    RUTINAS GENERALES
        ******************************************** */
        public DataTable ValidarTurnar(DataTable dtOrigen, int iArea, out string sAreasInternas)
        {
            sAreasInternas = null;
            DataTable newTable = dtOrigen.Clone();

            //RTPCLAVE, RTPDESCRIPCION, RTPFORMA

            for (int iIdx = 0; iIdx < dtOrigen.Rows.Count; iIdx++)
            {
                // VALIDAR EL TURNAR
                if (Convert.ToInt32(dtOrigen.Rows[iIdx]["RTPCLAVE"]) == 2)
                {
                    Dictionary<string, object> dicParam = new Dictionary<string, object>();
                    dicParam.Add(Constantes.Parametro.FECHA, DateTime.Now);
                    dicParam.Add(DButil.SIT_ADM_AREAHIST_COL.ARHREPORTA, iArea);

                    sAreasInternas = JsonTransform.convertJson(_sitDmlDbSer.operEjecutar<ConsultaDao>(nameof(ConsultaDao.dmlSelectAreasActualReporta), dicParam));

                    if (sAreasInternas != JsonTransform.NO_RECORDS)
                    {
                        newTable.Rows.Add(dtOrigen.Rows[iIdx]["RTPCLAVE"], dtOrigen.Rows[iIdx]["RTPDESCRIPCION"], dtOrigen.Rows[iIdx]["RTPFORMA"]);
                    }
                }
                else
                {
                    newTable.Rows.Add(dtOrigen.Rows[iIdx]["RTPCLAVE"], dtOrigen.Rows[iIdx]["RTPDESCRIPCION"], dtOrigen.Rows[iIdx]["RTPFORMA"]);
                }
            }        
            return newTable;
        }
        //dmlSelectRespuestaTranspuesta

        [HttpPost]
        public IActionResult BuscarRespuesta(int repClaveD)
        {
            string sRes  = JsonTransform.convertJson(_sitDmlDbSer.operEjecutar<ConsultaDao>(nameof(ConsultaDao.dmlSelectRespuestaTranspuesta), repClaveD));            

            return Content(sRes);
        }
    }
}
