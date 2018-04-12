using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using SFP.SIT.SERV.Util;
using SFP.SIT.SERV.Dao.ADM;
using SFP.SIT.WEB.Services;
using SFP.SIT.WEB.Models;
using SFP.Persistencia.Servicio;
using SFP.SIT.WEB.Injection;
using SFP.Persistencia.Model;
using SFP.SIT.WEB.Util;
using SFP.SIT.SERV.Model.ADM;

namespace SFP.SIT.WEB.Controllers
{
    public class ChatController : Controller
    {
        IHostingEnvironment _app;
        protected readonly DmlDbSer _sitDmlDbSer;
        protected readonly ICacheWebSIT _memCache;


        public ChatController(IHostingEnvironment app, ICacheWebSIT memCache)
        {
            _app = app;
            _memCache = memCache;
            _sitDmlDbSer = new DmlDbSer(_memCache.ObtenerDato(CacheWebSIT.APP_BD_CONFIG) as BaseDbMdl);

        }

        [HttpGet]
        
        public IActionResult Index()
        {
            //enviar lista de usuarios disponibles
            var identity = (System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity;
            string currentUserId = identity.Name.ToString();

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(DButil.SIT_ADM_USUARIO_COL.USRCLAVE, currentUserId);
            List<UsuarioViewModel> UsersConnected = (List<UsuarioViewModel>)_sitDmlDbSer.operEjecutar<SeguridadSer>(nameof(SeguridadSer.EncontrarUsuarios), dicParam);

            ViewBag.ConnectedUsers = JsonTransform.convertJson(UsersConnected as List<UsuarioViewModel>);
            ViewBag.YourId = currentUserId;
            //ViewBag.YourId = UsersConnected.;
            return View();
            
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Getmessages()
        {
            var identity = (System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity;
            string currentUserId = identity.Name.ToString();

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(DButil.SIT_ADM_USUARIO_COL.USRCLAVE, currentUserId);
            UsuarioViewModel Messages = (UsuarioViewModel)_sitDmlDbSer.operEjecutar<SeguridadSer>(nameof(SeguridadSer.MensajesUsuario), dicParam);

            var ConnectedUsers = JsonTransform.convertJson(Messages as UsuarioViewModel);
            return Json(ConnectedUsers);
            //return ConnectedUsers;
        }

        /*rECIVE EL json DE LA CONVERSACION Y LO GUARDA EN AMBOS REGISTROS (EMISOR, RECEPTOR)*/
        [HttpPost]
        public IActionResult SendMessage(string conversation, string to)
        {
            var identity = (System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity;
            string currentUserId = identity.Name.ToString();

            SIT_ADM_USUARIO usrMdl = new SIT_ADM_USUARIO() {
                 usractivo = conversation,
                 usrclave = Int32.Parse(to)
            };

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(DButil.SIT_ADM_USUARIO_COL.USRCLAVE, Int32.Parse(to));
            int ires = (int)_sitDmlDbSer.operEjecutar<SeguridadSer>(nameof(SeguridadSer.UpdateConversation), usrMdl);
            //usrMdl.usrclave =  Int32.Parse(currentUserId);
            //ires = (int)_sitDmlDbSer.operEjecutar<SeguridadSer>(nameof(SeguridadSer.UpdateConversation), usrMdl);
            return Json(ires);
        }


    }
}