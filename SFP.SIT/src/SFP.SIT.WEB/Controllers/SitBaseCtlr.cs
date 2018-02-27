using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SFP.Persistencia.Model;
using SFP.Persistencia.Servicio;
using SFP.SIT.SERV.Model.ADM;
using SFP.SIT.WEB.Injection;
using SFP.SIT.WEB.Models;
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
    }
}