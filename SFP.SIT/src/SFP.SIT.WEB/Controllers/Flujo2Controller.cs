using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SelectPdf;
using SFP.Persistencia.Model;
using SFP.SIT.AFD.Core;
using SFP.SIT.AFD.Model;
using SFP.SIT.SERV.Dao;
using SFP.SIT.SERV.Dao.ADM;
using SFP.SIT.SERV.Dao.DOC;
using SFP.SIT.SERV.Dao.RED;
using SFP.SIT.SERV.Dao.RESP;
using SFP.SIT.SERV.Dao.SNT;
using SFP.SIT.SERV.Dao.SOL;
using SFP.SIT.SERV.Model.ADM;
using SFP.SIT.SERV.Model.DOC;
using SFP.SIT.SERV.Model.RED;
using SFP.SIT.SERV.Model.RESP;
using SFP.SIT.SERV.Model.SOL;
using SFP.SIT.SERV.Negocio;
using SFP.SIT.SERV.Util;
using SFP.SIT.WEB.Injection;
using SFP.SIT.WEB.Models;
using SFP.SIT.WEB.Services;
using SFP.SIT.WEB.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace SFP.SIT.WEB.Controllers
{
    public class Flujo2Controller : SitBaseCtlr
    {
        SolicitudSer _solServ;

        public Flujo2Controller(ICacheWebSIT memCache, IHttpContextAccessor httpContextAccessor, ILogger<Flujo2Controller> logger, IHostingEnvironment app)
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



        [HttpGet]
        public IActionResult PVasignar()
        {
            _appLog.opdesc = "PVasignar ";
            _appLog.data = "";
            // aqui buscamods...
            return View();
        }

        [HttpPost]
        public IActionResult PVasignar(long folio, long nodo, int servPub, int tipoArista)
        {
            AfdEdoDataMdl afdDataMdl = NodoActualIni(folio, tipoArista, nodo);
            afdDataMdl.usrClaveDestino = servPub;

            afdDataMdl.ID_AreaUT = (int)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.UT);

            afdDataMdl.dicAfdFlujo = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO) as Dictionary<Int32, AfdNodoFlujo>;

            object oResultado = _sitDmlDbSer.operEjecutarTransaccion<AfdServicio>(nameof(AfdServicio.Accion), afdDataMdl);
            _appLog.opdesc = "PVturnar - AfdServicio.OPE_TURNAR ";
            _appLog.data = afdDataMdl.Datos();

            return RedirectToAction("BandejaEntrada", "Solicitud");
        }




        ////////[HttpGet]
        ////////public IActionResult PVrespSimple()
        ////////{
        ////////    EdoActViewModel edoActVM = new EdoActViewModel();
        ////////    edoActVM.controlName = "Flujo" + _memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.FLUJO).ToString();
        ////////    edoActVM.accion = AfdConstantes.RESPUESTA.AMPLIACION;

        ////////    _appLog.opdesc = "PVrespSimple ";
        ////////    _appLog.data = "";

        ////////    return View(edoActVM);
        ////////}

        ////////[HttpPost]
        ////////public IActionResult PVrespSimple(Int64 folio, Int64 nodo, Int32 tipoArista, string lstArt7, int tipoInfo, string respuesta, string Oficio, ICollection<IFormFile> ArchivoResolucion)        
        ////////{
        ////////    AfdEdoDataMdl afdDataMdl = NodoActualIni(folio,  tipoArista, nodo);

        ////////    //////////////////////////afdDataMdl.AFDresolucionMdl = new SIT_ARISTA_RESOLUCION( megclave: Constantes.ModoEntrada.NO_ESPECIFICADO, ariclave: Constantes.General.ID_PENDIENTE, rsl_art7: lstArt7, rsl_tam_cant_dir: "", rsl_ubicacion: "", nfoclave: tipoInfo);
                
        ////////    afdDataMdl.Observacion = respuesta.Replace("\"", "\\\"");

        ////////    afdDataMdl.lstDocContenidoMdl = DocumentoGestionar(Oficio, ref ArchivoResolucion);
        ////////    afdDataMdl.ID_AreaDestino = (int) _memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.UT);
        ////////    afdDataMdl.usrClaveDestino = (int)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.USR_TRANSPARENCIA);
        ////////    afdDataMdl.ID_AreaInai = (Int32)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.INAI);
        ////////    afdDataMdl.ID_AreaUT = (Int32)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.UT);

        ////////    // TODAS LAS RESPUESTAS VAN AL PERFIL DEL AREA DE TRANSPARENCIA.....                           
        ////////    if (tipoArista == AfdConstantes.RESPUESTA.NEGAR_AMPLIACION)
        ////////    {
        ////////        List<SIT_RED_ARISTA> lstAristaMdl = _solServ.ObtenerAristasIDnodoDestino(folio, nodo);
        ////////        SIT_RED_NODO nodoAnterior = _sitDmlDbSer.operEjecutar<SIT_RED_NODODao>(nameof(SIT_RED_NODODao.dmlSelectNodoID), lstAristaMdl[0].nodorigen) as SIT_RED_NODO;
        ////////    }

        ////////    List <SIT_ADM_USUARIO> lstUsuarios = _sitDmlDbSer.operEjecutar<SIT_ADM_USUARIODao>(nameof(SIT_ADM_USUARIODao.dmlSelectUsuarioComite), null) as List<SIT_ADM_USUARIO>;
        ////////    if (lstUsuarios != null)
        ////////    {
        ////////        afdDataMdl.usrClaveDestino = lstUsuarios[0].usrclave;
        ////////    }            
        ////////    // COLOCAMOS A LAPEROSNA QUE TIEN U N PËRFIL DE COMITE DE TRNASPARENCIA
        ////////    afdDataMdl.dicAfdFlujo = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO) as Dictionary<Int32, AfdNodoFlujo>;
        ////////    object oResultado = _sitDmlDbSer.operEjecutarTransaccion<AfdServicio>(nameof(AfdServicio.Accion), afdDataMdl);

        ////////    _appLog.opdesc = "PVrespSimple - AfdServicio.OPE_ACCION";
        ////////    _appLog.data = afdDataMdl.Datos();

        ////////    if (_sitDmlDbSer.MsjError != null)
        ////////    {
        ////////        // mas un mensaje de eror
        ////////        return View();
        ////////    }
        ////////    else
        ////////        return RedirectToAction("BandejaEntrada", "Solicitud");

        ////////}

        [HttpGet]
        public IActionResult PVanalizarResp()
        {
            _appLog.opdesc = "PVanalizarResp ";
            _appLog.data = "";

            return View();
        }        

        [HttpPost]       
        public FileResult PVpublicaPdf( string oficio)
        {
            // TERMINAR 
            // TERMINAR 
            // TERMINAR 
            ////SessionUsrMdl usrMdl = (SessionUsrMdl)System.Web.HttpContext.Current.Session[ConstantesWeb.SESSION_USUARIO];
            ////logger.Info(new WebAppLog(Request.UserHostAddress, usrMdl.usuNombre, ConstantesWeb.LOG_FLUJO_PVpublicaPDF_OPE, "PVpublicaPdf(string oficio)", AfdFlujoTrabajoSer.OPE_ACCION,
            ////    "AfdFlujoTrabajoSer.OPE_ACCION", "{\"oficio\":" + oficio + ",\"}"));

            PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize), "A4", true);
            PdfPageOrientation pdfOrientation = (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation), "Portrait", true);

            int webPageWidth = 1024;
            int webPageHeight = 780;

            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            converter.Options.PdfPageSize = pageSize;
            converter.Options.PdfPageOrientation = pdfOrientation;
            converter.Options.WebPageWidth = webPageWidth;
            converter.Options.WebPageHeight = webPageHeight;

            // create a new pdf document converting an urlbaseUrl
            PdfDocument doc;

            if (oficio != null)
                doc = converter.ConvertHtmlString(oficio, "");
            else
                doc = converter.ConvertHtmlString(" La respuesta se encuentra en blanco", "");

            byte[] fileBytes = doc.Save();
            string fileName = "Documento.pdf";
            return File(fileBytes, "application/pdf", fileName);
        }


        /*********************************************
               UNIDAD  TRANSPARENCIA 
        ******************************************** */


        [HttpGet]
        public IActionResult UTnotificarAmp()
        {
            _appLog.opdesc = "UTnotificarAmp ";
            _appLog.data = "";
            return View();
        }
        
        [HttpGet]
        public IActionResult PVnotificarAmp(long folio, int tipoPrcActual, long claNodo)
        {
            _appLog.opdesc = "PVnotificarAmp ";
            _appLog.data = " { PVnotificarAmp : { \"folio\":" + folio + ", \"tipoPrcActual\":" + tipoPrcActual + ", \"claNodo\":" + claNodo + " } }";
                       
            EdoActViewModel edoActVM = new EdoActViewModel();      
            // PUEDE HABER VARIAS SOLICITUDES DE AMPLIACION DE PLAZO PERO CON UNA ES SUFICIENTE
            List<SIT_RED_ARISTA> lstAristaMdl = _solServ.ObtenerAristasIDnodoDestino(folio, claNodo);
            ////////////////////////////////edoActVM.respNotificarVM = _solServ.ObtenerRespNotificar(folio, lstAristaMdl[0].ariclave, tipoPrcActual, nameof(SIT_RED_ARISTADao.dmlSelectRespDocAristaAnt));
            return View(edoActVM);
        }

        /* PV NOTICAR LA AMPLIACION  ES DIFERENTE...*/
        [HttpGet]
        public IActionResult PVnotificar(long folio, int tipoPrcActual, long claNodo)
        {
            _appLog.opdesc = "PVnotificar ";
            _appLog.data = " { PVnotificar : { \"folio\":" + folio + ", \"tipoPrcActual\":" + tipoPrcActual  + ", \"claNodo\":" + claNodo + " } }";

            EdoActViewModel edoActVM = new EdoActViewModel();

            
            SIT_SOL_SEGUIMIENTO segMdl = _solServ.ObtenerSeguimiento(folio, tipoPrcActual);
            ////////////////if (segMdl.ariclave == 0)
            ////////////////{
                // SOLO EXISTE UN ARISTA ANTERIOR
                List<SIT_RED_ARISTA> lstAristaMdl = _solServ.ObtenerAristasIDnodoDestino(folio, claNodo);
            /////////////////////////////////////edoActVM.respNotificarVM = _solServ.ObtenerRespNotificar(folio, lstAristaMdl[0].ariclave, tipoPrcActual, nameof(SIT_RED_ARISTADao.dmlSelectRespDocAristaAnt));
            //////////////////}
            //////////////////else
            //////////////////////////////////////edoActVM.respNotificarVM = _solServ.ObtenerRespNotificar(folio, segMdl.ariclave, tipoPrcActual, nameof(SIT_RED_ARISTADao.dmlSelectRespDoc));

            if (edoActVM.respNotificarVM != null)
            {
                if (edoActVM.respNotificarVM.respTipoArista == Constantes.RespuestaEstado.ACEPTADA)
                    edoActVM.tipoAristaInfomex = _solServ.ObtenerAristaTipoID(Constantes.Respuesta.NOTIFICACION_PRORROGA);

                //// VERIFICAR CAUELS SON LOS INTERNOS Y LO S EXERNOS
                ////////////////else
                //////////////////////edoActVM.tipoAristaInfomex = _solServ.ObtenerAristaTipo(Constantes.Respuesta_TIPO.EXTERNO, Constantes.Respuesta.NOTIFICACION_PRORROGA.ToString());
                ////////////////}
                ////////////////else
                ////////////////    edoActVM.tipoAristaInfomex = _solServ.ObtenerAristaTipo(Constantes.Respuesta_TIPO.EXTERNO, Constantes.Respuesta.NOTIFICACION_PRORROGA.ToString());
            }
            return View(edoActVM);
        }

        [HttpPost]
        public IActionResult PVnotificar(Int64 folio, Int64 nodo, int tipoArista, int respInfomex)
        {
            AfdEdoDataMdl afdDataMdl = NodoActualIni( folio,  tipoArista, nodo);
            //////////////afdDataMdl.AFDseguimientoMdl.segfecfin = DateTime.Now;
            //////////////afdDataMdl.AFDseguimientoMdl.rtpclave = respInfomex;

            ////int iSiguienteEstado = _memCacheSIT.EstadoFinal(afdDataMdl.AFDnodoActMdl.nodclave, tipoArista);
            ////Dictionary<int, int> dicFlujoEstadoPerfil = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_NODO_ESTADO_PERFIL) as Dictionary<int, int>;
            ////int iPerfil = dicFlujoEstadoPerfil[iSiguienteEstado];
            ////afdDataMdl.ID_AreaDestPerfil = iPerfil;
            ////afdDataMdl.ID_AreaDestino = _solServ.ObtenerAreaPerfil(iPerfil);
            ////object oResultado = EjecutarAfdServicio(ref afdDataMdl, AfdServicio.OPE_ACCION);

            afdDataMdl.dicAfdFlujo = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO) as Dictionary<Int32, AfdNodoFlujo>;
            object oResultado = _sitDmlDbSer.operEjecutarTransaccion<AfdServicio>(nameof(AfdServicio.Accion), afdDataMdl);

            _appLog.opdesc = "PVnotificar - Post - AfdServicio.OPE_ACCION";
            _appLog.data = afdDataMdl.Datos();

            return RedirectToAction("BandejaEntrada", "Solicitud");
        }








        // ESTO ES OTRA  RUTINA PARA AVANZAR
        // ESTO ES OTRA  RUTINA PARA AVANZAR
        // ESTO ES OTRA  RUTINA PARA AVANZAR
        // ESTO ES OTRA  RUTINA PARA AVANZAR
        // ESTO ES OTRA  RUTINA PARA AVANZAR
        // ESTO ES OTRA  RUTINA PARA AVANZAR

        ////object oResultado = _sitDmlDbSer.operEjecutarTransaccion<AfdServicio>(nameof(AfdServicio.Accion), afdDataMdl);

        // creo una transaccion yu grabo los documentois.,..

        // TODO CAMBIO,..
        // responderVM.nodclave

        //////AfdEdoDataMdl afdDataMdl = NodoActualIni(folio, tipoArista, nodo);
        //////afdDataMdl.AFDresolucionMdl = new SIT_ARISTA_RESOLUCION(
        //////    megclave: Constantes.ModoEntrada.NO_ESPECIFICADO, ariclave: Constantes.General.ID_PENDIENTE,
        //////    rsl_art7: lstArt7, rsl_tam_cant_dir: " ", rsl_ubicacion: " ", nfoclave: tipoInfo);
        //////afdDataMdl.Observacion = respuesta.Replace("\"", "\\\"");
        //////afdDataMdl.lstDocContenidoMdl = DocumentoGestionar(Oficio, ref ocArchivos);
        //////afdDataMdl.ID_AreaDestino = (int)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.UT);
        //////afdDataMdl.usrClaveDestino = (int)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.USR_TRANSPARENCIA);
        //////afdDataMdl.ID_AreaInai = (Int32)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.INAI);
        //////afdDataMdl.ID_AreaUT = (Int32)_memCacheSIT.ObtenerDato(Constantes.CfgClavesRegistro.UT);

        //////afdDataMdl.dicAfdFlujo = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO) as Dictionary<Int32, AfdNodoFlujo>;
        //////object oResultado = _sitDmlDbSer.operEjecutarTransaccion<AfdServicio>(nameof(AfdServicio.Accion), afdDataMdl);

        //////_appLog.opdesc = "PVpublica - AfdServicio.OPE_ACCION";
        //////_appLog.data = afdDataMdl.Datos();
    }
}
