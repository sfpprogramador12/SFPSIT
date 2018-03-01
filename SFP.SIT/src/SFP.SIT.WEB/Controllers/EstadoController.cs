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
using SFP.Persistencia.Model;
using SFP.SIT.SERV.Model.RESP;

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


        /*********************************************
               FUNCIONES GENERALES PARA LOS NODOS
        ******************************************** */
        public AfdEdoDataMdl NodoActualIni(Int64 folio, Int32 tipoArista, Int64 nodo)
        {
            AfdEdoDataMdl afdDataMdl = new AfdEdoDataMdl();

            afdDataMdl.solClave = folio;
            afdDataMdl.ID_AreaInai = (Int32)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.INAI);
            afdDataMdl.ID_AreaUT = (Int32)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.UT);

            afdDataMdl.usrClaveOrigen = _iUsuario;
            afdDataMdl.FechaRecepcion = DateTime.Now;
            afdDataMdl.rtpclave = tipoArista;
            afdDataMdl.dicDiaNoLaboral = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_DIA_NO_LABORAL) as Dictionary<Int64, char>;
            afdDataMdl.lstProcesoPlazos = _memCacheSIT.ObtenerDato(CacheWebSIT.LST_SOL_PROCESOPLAZOS) as List<SIT_SOL_PROCESOPLAZOS>;
            afdDataMdl.SharePointCxn = _memCacheSIT.ObtenerDato(CacheWebSIT.APP_SHAREPOINT_CONFIG) as CfgSharePointMdl;

            // BUSCAR DATOS PARA PROCESAR LA ACCION
            afdDataMdl.AFDnodoActMdl = _sitDmlDbSer.operEjecutar<SIT_RED_NODODao>(nameof(SIT_RED_NODODao.dmlSelectNodoID), nodo) as SIT_RED_NODO;

            afdDataMdl.ID_EstadoActual = (int)afdDataMdl.AFDnodoActMdl.nedclave;
            afdDataMdl.AFDseguimientoMdl = _solServ.ObtenerSeguimiento(afdDataMdl.solClave, (int)afdDataMdl.AFDnodoActMdl.prcclave);
            afdDataMdl.solicitud = _solServ.ObtenerSolicitudID(afdDataMdl.solClave);
            afdDataMdl.ID_ClaAfd = (Int32)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.FLUJO);
            afdDataMdl.ID_Capa = afdDataMdl.AFDnodoActMdl.nodcapa;
            return afdDataMdl;
        }

        public EdoActViewModel ObtenerDatosGral(long nodo, bool bSolicitante, int iPrc, int iArea, int iPerfil)
        {
            EdoActViewModel edoActVM = new EdoActViewModel();
            DateTime dteFecAclaracion = new DateTime();
            CalcularPlazoNeg calcularPlazoNeg = new CalcularPlazoNeg(_memCacheSIT.ObtenerDato(CacheWebSIT.DIC_DIA_NO_LABORAL) as Dictionary<Int64, char>);

            SIT_RED_NODO redNodo = _sitDmlDbSer.operEjecutar<SIT_RED_NODODao>(nameof(SIT_RED_NODODao.dmlSelectNodoID), nodo) as SIT_RED_NODO;

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
                    DateTime dtFechaLimite = calcularPlazoNeg.ObtenerFechaFinal(solSolicitud.solfecrec, edoAccionMdl.plazo);
                    dtFechaLimite = dtFechaLimite.AddMonths(-1);
                    //edoAccionMdl.fecLimite = dtFechaLimite.ToString("yyyy/MM/dd");
                    edoAccionMdl.fecLimite = "2018/12/31";
                }
                else if (edoAccionMdl.id == Constantes.Respuesta.ASIGNAR)
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

                    if (iPerfil == Constantes.Perfil.UA )
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
        public IActionResult PRUDrevisarResp(int iPrc, Int64 lNodo, int iArea, int iPerfil, int iUsr)
        {
            EdoActViewModel edoActVM = ObtenerDatosGral(lNodo, true, iPrc, iArea, iPerfil);
            edoActVM.nodoView = "PRUDrevisarResp";

            ViewBag.DatosGrals = edoActVM;

            return View();
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

            // Tengo multiples respuesta en la UA analizar...                        
            SIT_RED_NODO redNodo = _sitDmlDbSer.operEjecutar<SIT_RED_NODODao>(nameof(SIT_RED_NODODao.dmlSelectNodoID), lNodo) as SIT_RED_NODO;
            

            List<AfdEdoPdoMdl> lstNvoAccion = new List<AfdEdoPdoMdl>();
            for (int iIdxAccion= 0; iIdxAccion < edoActVM.lstEdoAccion.Count; iIdxAccion++)
            {
                if (edoActVM.lstEdoAccion[iIdxAccion].id == Constantes.Respuesta.RESPUESTA_MULTIPLE)
                {
                    //NECESITO CLACULAR LA FEHCA LIMITE                            
                
                    List<SIT_RESP_TIPO> lstResptipo = _sitDmlDbSer.operEjecutar<SIT_RESP_TIPODao>(nameof(SIT_RESP_TIPODao.dmlSelectRespTipo), 6) as List<SIT_RESP_TIPO>;
                    for (int iIdxResp = 0; iIdxResp < lstResptipo.Count; iIdxResp++)
                    {
                        lstNvoAccion.Add( new AfdEdoPdoMdl { id = lstResptipo[iIdxResp].rtpclave, plazo = lstResptipo[iIdxResp].rtpplazo,
                         fecLimite = "2018/12/31", forma = lstResptipo[iIdxResp].rtpforma, nivel = lstResptipo[iIdxResp].rtptipo, formato = lstResptipo[iIdxResp].rtpformato,
                            text = lstResptipo[iIdxResp].rtpdescripcion} );
                    }
                }
                else
                {
                    lstNvoAccion.Add(edoActVM.lstEdoAccion[iIdxAccion]);
                }
            }

            ViewBag.ListaAccion = JsonTransform.convertJson(lstNvoAccion);
        

            ViewBag.Nodo = lNodo;
            ViewBag.controlName = "Estado";

            List<NodoRespuestaMdl> lstRespuesta = _sitDmlDbSer.operEjecutar<ConsultaDao>(nameof(ConsultaDao.dmlSelectRespuestaNodo), lNodo) as List<NodoRespuestaMdl>;
            ViewBag.GridResp = JsonTransform.convertJson(lstRespuesta);


            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(DButil.SIT_RED_NODO_COL.NODCLAVE, lNodo);
            dicParam.Add(DButil.SIT_RED_NODORESP_COL.SDOCLAVE, Constantes.RespuestaEstado.ANALIZAR);

            // EL USUAIRO FIMRADO AUNQUE SE REQUIERE EN QUE ESTA SELECCIONADO..
            dicParam.Add(DButil.SIT_RESP_TURNAR_COL.USRCLAVE, _iUsuario);
            ViewBag.Instruccion = _sitDmlDbSer.operEjecutar<ConsultaDao>(nameof(ConsultaDao.dmlSelectInstrucciones), dicParam) as string;

            return View();
        }

        [HttpPost]
        public IActionResult UAanalizarResponder(long solclave, int proclave, long nodclave, int araclave, int perclave)
        {
            Dictionary<string, object> dicDatos = new Dictionary<string, object>();
            //// DATOS GENERALES A AGRUPAR....
            dicDatos.Add(ProcesoGralDao.PARAM_NODCLAVE, nodclave);
            dicDatos.Add(ProcesoGralDao.PARAM_SHAPOINTMDL, _memCacheSIT.ObtenerDato(CacheWebSIT.APP_SHAREPOINT_CONFIG) as CfgSharePointMdl);



            SIT_RED_NODORESP nodResp = new SIT_RED_NODORESP();
            nodResp.nodclave = nodclave;
            nodResp.sdoclave = Constantes.RespuestaEstado.PROPUESTA;


            AfdEdoDataMdl afdDataMdl = NodoActualIni(solclave, Constantes.Respuesta.ASIGNAR, nodclave);
            afdDataMdl.dicAfdFlujo = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO) as Dictionary<Int32, AfdNodoFlujo>;
            afdDataMdl.dicAuxRespuesta = dicDatos;

            List<SIT_RESP_RESPUESTA> dmlSelectRespEdo = _sitDmlDbSer.operEjecutar<ConsultaDao>(nameof(ConsultaDao.dmlSelectRespEdo), nodResp) as List<SIT_RESP_RESPUESTA>;
            // siempre tiene que haber uan respueta para avanzar...
            // Buscar tipos de repsuesta
            // * Solo permite TURNAR, Ampliacion de plazo y Respuesta Multiple..
            if (dmlSelectRespEdo[0].rtpclave == Constantes.Respuesta.TURNAR)
                afdDataMdl.rtpclave = Constantes.Respuesta.TURNAR;
            else if (dmlSelectRespEdo[0].rtpclave == Constantes.Respuesta.RIA_AREA)
                afdDataMdl.rtpclave = Constantes.Respuesta.RIA_AREA;
            else
                afdDataMdl.rtpclave = Constantes.Respuesta.RESPUESTA_MULTIPLE;

            //BUSCAR QUIEN ES EL USUARIO QUE ESTA EN EL EESTADO
            afdDataMdl.usrClaveDestino = _iUsuario;
            afdDataMdl.usrClaveOrigen = _iUsuario;
            afdDataMdl.usrClaveAusencia = _iUsuario;  // Aqui cambiar de acuerdo a la página WEB
            afdDataMdl.ID_PerfilDestino = Constantes.Perfil.PRUT;

            afdDataMdl.ID_AreaUT = (int)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.UT);
            afdDataMdl.dicAfdFlujo = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO) as Dictionary<Int32, AfdNodoFlujo>;
            string oResultado = _sitDmlDbSer.operEjecutarTransaccion<AfdServicio>(nameof(AfdServicio.Accion), afdDataMdl).ToString();

            return RedirectToAction("BandejaEntrada", "Solicitud");
        }


        [HttpPost]
        public IActionResult CtrlSiguienteEstado(Int64 solclave, Int32 proclave, Int64 nodclave, Int32 araclave, Int32 tipoArista)
        {

            ////AfdEdoDataMdl afdDataMdl = NodoActualIni(solclave, tipoArista, nodclave);

            //////////////////////////////afdDataMdl.AFDresolucionMdl = new SIT_ARISTA_RESOLUCION( megclave: Constantes.ModoEntrada.NO_ESPECIFICADO, ariclave: Constantes.General.ID_PENDIENTE, rsl_art7: lstArt7, rsl_tam_cant_dir: "", rsl_ubicacion: "", nfoclave: tipoInfo);

            ////afdDataMdl.Observacion = respuesta.Replace("\"", "\\\"");

            ////afdDataMdl.lstDocContenidoMdl = DocumentoGestionar(Oficio, ref ArchivoResolucion);
            ////afdDataMdl.ID_AreaDestino = (int)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.UT);
            ////afdDataMdl.usrClaveDestino = (int)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.USR_TRANSPARENCIA);
            ////afdDataMdl.ID_AreaInai = (Int32)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.INAI);
            ////afdDataMdl.ID_AreaUT = (Int32)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.UT);

            ////// TODAS LAS RESPUESTAS VAN AL PERFIL DEL AREA DE TRANSPARENCIA.....                           
            ////if (tipoArista == AfdConstantes.RESPUESTA.NEGAR_AMPLIACION)
            ////{
            ////    List<SIT_RED_ARISTA> lstAristaMdl = _solServ.ObtenerAristasIDnodoDestino(folio, nodo);
            ////    SIT_RED_NODO nodoAnterior = _sitDmlDbSer.operEjecutar<SIT_RED_NODODao>(nameof(SIT_RED_NODODao.dmlSelectNodoID), lstAristaMdl[0].nodorigen) as SIT_RED_NODO;
            ////}

            ////List<SIT_ADM_USUARIO> lstUsuarios = _sitDmlDbSer.operEjecutar<SIT_ADM_USUARIODao>(nameof(SIT_ADM_USUARIODao.dmlSelectUsuarioComite), null) as List<SIT_ADM_USUARIO>;
            ////if (lstUsuarios != null)
            ////{
            ////    afdDataMdl.usrClaveDestino = lstUsuarios[0].usrclave;
            ////}
            ////// COLOCAMOS A LAPEROSNA QUE TIEN U N PËRFIL DE COMITE DE TRNASPARENCIA
            ////afdDataMdl.dicAfdFlujo = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO) as Dictionary<Int32, AfdNodoFlujo>;
            ////object oResultado = _sitDmlDbSer.operEjecutarTransaccion<AfdServicio>(nameof(AfdServicio.Accion), afdDataMdl);

            ////_appLog.opdesc = "PVrespSimple - AfdServicio.OPE_ACCION";
            ////_appLog.data = afdDataMdl.Datos();


            ////Console.WriteLine(iValor);
            ////return RedirectToAction("BandejaEntrada", "Solicitud");
            return null;
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

    }

}
