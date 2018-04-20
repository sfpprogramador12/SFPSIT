using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SFP.Persistencia.Model;
using SFP.Persistencia.Servicio;
using SFP.SIT.AFD.Model;
using SFP.SIT.SERV.Dao;
using SFP.SIT.SERV.Dao.RED;
using SFP.SIT.SERV.Model.ADM;
using SFP.SIT.SERV.Model.RED;
using SFP.SIT.SERV.Model.SOL;
using SFP.SIT.SERV.Util;
using SFP.SIT.WEB.Injection;
using SFP.SIT.WEB.Models;
using SFP.SIT.WEB.Services;
using SFP.SIT.WEB.Util;
using System;
using System.Collections.Generic;

namespace SFP.SIT.WEB.Controllers
{
    public class SitBaseCtlr  : Controller
    {
        protected DmlDbSer _sitDmlDbSer;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly ICacheWebSIT _memCacheSIT;
        protected readonly IHostingEnvironment _app;
        protected List<SIT_ADM_MODULO> _lstAdmModMdl;

        protected ILogger _loggerAud;
        protected LogViewModel _appLog;

        int numop { set; get; }
        string opdesc { set; get; }
        string data { set; get; }
        protected int _iUsuario { set; get; }

        public SitBaseCtlr(ICacheWebSIT memCache, IHttpContextAccessor httpContextAccessor, ILogger loggerAud, IHostingEnvironment app)
        {            
            _memCacheSIT = memCache;
            _httpContextAccessor = httpContextAccessor;            
            _loggerAud = loggerAud;
            _app = app;
            _sitDmlDbSer = new DmlDbSer((BaseDbMdl) _memCacheSIT.ObtenerDato(CacheWebSIT.APP_BD_CONFIG));

            if (User != null)                
                _iUsuario = Convert.ToInt32(User.FindFirst(ConstantesWeb.Usuario.CLAVE).Value);
            else
                _iUsuario = 0;

            _appLog = new LogViewModel();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _appLog.direccionIP = filterContext.HttpContext.Connection.RemoteIpAddress.ToString();
            if (  User != null)
                _appLog.usuario = User.FindFirst(ConstantesWeb.Usuario.NOMBRE).Value;
            else
                _appLog.usuario = "anonimo";

            _appLog.objeto = filterContext.RouteData.Values["controller"].ToString();
            _appLog.accion = filterContext.RouteData.Values["action"].ToString();

            _loggerAud.LogInformation(_appLog.ToString());
            base.OnActionExecuted(filterContext);
        }

        public AfdEdoDataMdl NodoActualIni(Int64 folio, Int32 tipoArista, Int64 nodo)
        {
            SolicitudSer _solServ = new SolicitudSer(_sitDmlDbSer);

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


        /*********************************************
               FUNCIONES GENERALES PARA LOS NODOS
        ******************************************** */
        public AfdEdoDataMdl NodoActualIni(Int64 folio, Int32 tipoArista, Int64 nodo, int iUsrDest, int iUsrOrigen, int iUsrAusencia, int iPerfilDest)
        {
            SolicitudSer _solServ = new SolicitudSer(_sitDmlDbSer);
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


            Dictionary<string, object> dicDatos = new Dictionary<string, object>();
            dicDatos.Add(ProcesoGralDao.PARAM_NODCLAVE, nodo);
            dicDatos.Add(ProcesoGralDao.PARAM_SHAPOINTMDL, _memCacheSIT.ObtenerDato(CacheWebSIT.APP_SHAREPOINT_CONFIG) as CfgSharePointMdl);
            afdDataMdl.dicAfdFlujo = _memCacheSIT.ObtenerDato(CacheWebSIT.DIC_AFD_FLUJO) as Dictionary<Int32, AfdNodoFlujo>;
            afdDataMdl.dicAuxRespuesta = dicDatos;

            afdDataMdl.usrClaveDestino = iUsrDest;
            afdDataMdl.usrClaveOrigen = iUsrOrigen;
            afdDataMdl.usrClaveAusencia = iUsrAusencia;  // Aqui cambiar de acuerdo a la página WEB
            afdDataMdl.ID_PerfilDestino = iPerfilDest;

            return afdDataMdl;
        }


    }
}