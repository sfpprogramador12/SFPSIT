using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFP.SIT.WEB.Models;
using SFP.SIT.WEB.Models.AccountViewModels;
using SFP.SIT.WEB.Services;
using SFP.SIT.WEB.Util;
using Microsoft.AspNetCore.Http;
using SFP.Persistencia.Servicio;
using SFP.Persistencia.Model;
using SFP.SIT.WEB.Injection;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Hosting;
using SFP.SIT.SERV.Util;
using SFP.SIT.SERV.Model.ADM;
using SFP.SIT.SERV.Dao.ADM;
using System.Linq;

namespace SFP.SIT.WEB.Controllers
{
    public class AccountController : Controller
    {        
        protected readonly DmlDbSer _sitDmlDbSer;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly ILogger _loggerAud;
        protected readonly ICacheWebSIT _memCache;
        protected readonly IHostingEnvironment _app;

        private LogViewModel _appLog;

        public AccountController( ICacheWebSIT memCache, IHttpContextAccessor httpContextAccessor, 
            ILogger<AccountController> logger, IHostingEnvironment app)
        {
            _memCache = memCache;
            _httpContextAccessor = httpContextAccessor;
            _app = app;

            _sitDmlDbSer = new DmlDbSer( _memCache.ObtenerDato(CacheWebSIT.APP_BD_CONFIG) as BaseDbMdl );

            _loggerAud = logger;
            _appLog = new LogViewModel();
            _appLog.direccionIP = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            _appLog.objeto = "AccountController";
        }


        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            _appLog.accion = "Login";
            if (User.Identity.IsAuthenticated)
            {
                _appLog.opdesc = ConstantesWeb.LogMensajes.USUARIO_SESSION;
                _appLog.usuario = User.FindFirst(ConstantesWeb.Usuario.NOMBRE).Value;
                _loggerAud.LogInformation(_appLog.ToString());
                return RedirectToAction("Plazos", "Informacion");
            }                
            else
            {                
                _appLog.opdesc = ConstantesWeb.LogMensajes.USUARIO_SESSION_EXPIRO;
                _loggerAud.LogInformation(_appLog.ToString());                
                return View(new LoginViewModel());
            }
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            _appLog.accion = "Login - POST";
            if (ModelState.IsValid)
            {
                Dictionary<string, object> dicParam = new Dictionary<string, object>();

                dicParam.Add(DButil.SIT_ADM_USUARIO_COL.USRCORREO, model.Email);
                dicParam.Add(DButil.SIT_ADM_USUARIO_COL.USRCONTRASEÑA, model.Password);
                dicParam.Add(SIT_ADM_USUARIOAREADao.PARAM_FECHA, DateTime.Now );
                dicParam.Add(SeguridadSer.PARAM_IP, Request.HttpContext.Connection.RemoteIpAddress.ToString());

                UsuarioViewModel usrMdl = (UsuarioViewModel)_sitDmlDbSer.operEjecutar<SeguridadSer>(nameof(SeguridadSer.ValidarUsuario), dicParam );
                model.Password = "";
                _appLog.data = JsonTransform.convertJson(model).ToString();

                if (usrMdl == null)
                {
                    model.Mensaje = "Existe un error en el sistema";
                    _appLog.opdesc = "ERROR AL AutentificarSer.OPE_VALIDAR_USUARIO -" + ConstantesWeb.LogMensajes.USUARIO_ERROR;
                }
                else if (usrMdl.AdmUsuMdl == null)
                {
                    model.Mensaje = "No existe el usuario o error en la contraseña";
                    _appLog.opdesc = "AutentificarSer.OPE_VALIDAR_USUARIO -" + ConstantesWeb.LogMensajes.USUARIO_NO_EXISTE;
                }
                else
                {
                    // EL USAURIO EXISTE..
                    _appLog.usuario = usrMdl.UsuarioNombre;

                    List<Claim> userClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, Convert.ToString(usrMdl.AdmUsuMdl.usrclave)),
                        new Claim(ConstantesWeb.Usuario.CLAVE, Convert.ToString(usrMdl.AdmUsuMdl.usrclave)),
                        new Claim(ConstantesWeb.Usuario.NOMBRE, usrMdl.UsuarioNombre),
                        new Claim(ConstantesWeb.Usuario.AREAS,  JsonTransform.Serializar(usrMdl.lstAreas)),
                        new Claim(ConstantesWeb.Usuario.PERFILES,JsonTransform.Serializar(usrMdl.lstPerfil)),
                        new Claim(ConstantesWeb.Usuario.MODULOS,JsonTransform.Serializar(usrMdl.lstModulo)),
                        new Claim(ConstantesWeb.Usuario.CORREO, usrMdl.AdmUsuMdl.usrcorreo ),
                        new Claim(ConstantesWeb.Usuario.CBOPERFILAREA, usrMdl.sCboPerfilArea )
                    };

                    /* Traer usuarios que comparte y guardarlos en el context*/
                    usrMdl.usuarioBase = new Dictionary<int, string>() {
                            { usrMdl.AdmUsuMdl.usrclave, usrMdl.UsuarioNombre }
                        };
                    usrMdl.usuariosCompartidos =  (Dictionary<int, string>)_sitDmlDbSer.operEjecutar<SeguridadSer>(nameof(SeguridadSer.GetUsuariosCompartidos), usrMdl.AdmUsuMdl.usrclave.ToString());
                    HttpContext.SetDataToSession<UsuarioViewModel>("User", usrMdl);
                    

                    ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(userClaims, "local"));
                    await HttpContext.Authentication.SignInAsync("SitCookieMiddlewareInstance", principal,
                        new AuthenticationProperties
                        {
                            ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                            IsPersistent = false,
                            AllowRefresh = false
                        });
                    HttpContext.User = principal;

                    _appLog.opdesc = "AutentificarSer.OPE_VALIDAR_USUARIO -" + ConstantesWeb.LogMensajes.USUARIO_AUTENTIFICADO;
                    return RedirectToAction("Plazos", "Informacion");
                }
            }         
            _loggerAud.LogInformation(_appLog.ToString());
            return View(model);
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        // <-- Solo administradores pueden acceder a esta parte
        public async Task<IActionResult> ImpersonateUser(String userId)
        {
            var identity = (System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity;
            string currentUserId = identity.Name.ToString();
            
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            // sign out the current user
            await HttpContext.Authentication.SignOutAsync("SitCookieMiddlewareInstance");

            Dictionary<string, object> dicParam = new Dictionary<string, object>();
            dicParam.Add(DButil.SIT_ADM_USUARIO_COL.USRCLAVE, userId);
            dicParam.Add(SIT_ADM_USUARIOAREADao.PARAM_FECHA, DateTime.Now);
            dicParam.Add(SeguridadSer.PARAM_IP, Request.HttpContext.Connection.RemoteIpAddress.ToString());
            UsuarioViewModel impersonatedUser  = (UsuarioViewModel)_sitDmlDbSer.operEjecutar<SeguridadSer>(nameof(SeguridadSer.EncontrarUsuario), dicParam);


            List<Claim> userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Convert.ToString(impersonatedUser.AdmUsuMdl.usrclave)),
                new Claim(ConstantesWeb.Usuario.CLAVE, Convert.ToString(impersonatedUser.AdmUsuMdl.usrclave)),
                new Claim(ConstantesWeb.Usuario.NOMBRE, impersonatedUser.UsuarioNombre),
                new Claim(ConstantesWeb.Usuario.AREAS,  JsonTransform.Serializar(impersonatedUser.lstAreas)),
                new Claim(ConstantesWeb.Usuario.PERFILES,JsonTransform.Serializar(impersonatedUser.lstPerfil)),
                new Claim(ConstantesWeb.Usuario.MODULOS,JsonTransform.Serializar(impersonatedUser.lstModulo)),
                new Claim(ConstantesWeb.Usuario.CORREO, impersonatedUser.AdmUsuMdl.usrcorreo ),
                new Claim(ConstantesWeb.Usuario.CBOPERFILAREA, impersonatedUser.sCboPerfilArea ),
                new Claim(ConstantesWeb.Usuario.USUARIOBASE, currentUserId)
                        
            };

            ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(userClaims, "local"));
            await HttpContext.Authentication.SignInAsync("SitCookieMiddlewareInstance", principal,
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                    IsPersistent = false,
                    AllowRefresh = false
                });
            HttpContext.User = principal;

            _appLog.opdesc = "AutentificarSer.OPE_VALIDAR_USUARIO -" + ConstantesWeb.LogMensajes.USUARIO_AUTENTIFICADO;
            return RedirectToAction("Plazos", "Informacion");

           
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        // <-- Solo administradores pueden acceder a esta parte
        public async Task<IActionResult> StopImpersonation()
        {
            var usuarioBase = ConstantesWeb.Usuario.USUARIOBASE;
            if (usuarioBase == "UsuBase")
            {
                return RedirectToAction("Login", "Account");
            }

            Dictionary<string, object> dicParam = new Dictionary<string, object>();

            dicParam.Add(DButil.SIT_ADM_USUARIO_COL.USRCLAVE, usuarioBase);
            dicParam.Add(SIT_ADM_USUARIOAREADao.PARAM_FECHA, DateTime.Now);
            dicParam.Add(SeguridadSer.PARAM_IP, Request.HttpContext.Connection.RemoteIpAddress.ToString());
            UsuarioViewModel impersonatedUser = (UsuarioViewModel)_sitDmlDbSer.operEjecutar<SeguridadSer>(nameof(SeguridadSer.EncontrarUsuario), dicParam);


            // sign out the current user
            await HttpContext.Authentication.SignOutAsync("SitCookieMiddlewareInstance");

            List<Claim> userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Convert.ToString(impersonatedUser.AdmUsuMdl.usrclave)),
                new Claim(ConstantesWeb.Usuario.CLAVE, Convert.ToString(impersonatedUser.AdmUsuMdl.usrclave)),
                new Claim(ConstantesWeb.Usuario.NOMBRE, impersonatedUser.UsuarioNombre),
                new Claim(ConstantesWeb.Usuario.AREAS,  JsonTransform.Serializar(impersonatedUser.lstAreas)),
                new Claim(ConstantesWeb.Usuario.PERFILES,JsonTransform.Serializar(impersonatedUser.lstPerfil)),
                new Claim(ConstantesWeb.Usuario.MODULOS,JsonTransform.Serializar(impersonatedUser.lstModulo)),
                new Claim(ConstantesWeb.Usuario.CORREO, impersonatedUser.AdmUsuMdl.usrcorreo ),
                new Claim(ConstantesWeb.Usuario.CBOPERFILAREA, impersonatedUser.sCboPerfilArea ),
                new Claim(ConstantesWeb.Usuario.USUARIOBASE, "UsuBase")

            };

            ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(userClaims, "local"));
            await HttpContext.Authentication.SignInAsync("SitCookieMiddlewareInstance", principal,
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                    IsPersistent = false,
                    AllowRefresh = false
                });
            HttpContext.User = principal;

            _appLog.opdesc = "AutentificarSer.OPE_VALIDAR_USUARIO -" + ConstantesWeb.LogMensajes.USUARIO_AUTENTIFICADO;
            return RedirectToAction("Plazos", "Informacion");


        }


        // GET: /Account/ResetPassword
        [HttpGet]        
        // public IActionResult ResetPassword(string code = null)
        // return code == null ? View("Error") : View();
        public IActionResult ResetPassword ()
        {
            UsuContraseñaViewModel usuCtrVM = new UsuContraseñaViewModel();
            usuCtrVM.correo =  User.FindFirst(ConstantesWeb.Usuario.CORREO).Value;
            return View(usuCtrVM);
        }

        [HttpPost]
        public IActionResult ResetPassword(string contraseña, string confirmar)
        {
            SIT_ADM_USUARIO usrMdl = new SIT_ADM_USUARIO();
            UsuContraseñaViewModel usuCtrVM = new UsuContraseñaViewModel();

            usrMdl.usrcorreo = User.FindFirst(ConstantesWeb.Usuario.CORREO).Value;

            if (contraseña == null)
                return View(usrMdl);

            if (contraseña != confirmar)
            {
                usuCtrVM.error = "La contraseña y la confirmación son diferentes, favor de corregir";
                usuCtrVM.correo = User.FindFirst(ConstantesWeb.Usuario.CORREO).Value;
                return View(usuCtrVM);
            }
                
            usrMdl.usrcontraseña = contraseña;
            usrMdl.usrclave = Convert.ToInt32(User.FindFirst(ConstantesWeb.Usuario.CLAVE).Value);
            int ires = (int)_sitDmlDbSer.operEjecutar<SeguridadSer>(nameof(SeguridadSer.CambiarContraseña), usrMdl);

            _appLog.opdesc = "ResetPassword";
            _appLog.data = "";

            if (ires > 0)
                return RedirectToAction("ResetPasswordConfirmation");
            else
            {
                usuCtrVM.correo = User.FindFirst(ConstantesWeb.Usuario.CORREO).Value;
                return View(usuCtrVM);
            }
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //[HttpPost]
        // POST: /Account/LogOff
        //[HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.Authentication.SignOutAsync("SitCookieMiddlewareInstance");
                _appLog.opdesc = "AutentificarSer.OPE_VALIDAR_USUARIO -" + ConstantesWeb.LogMensajes.USUARIO_SALIR;
            }
            return RedirectToAction("Login");
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            //////// Request a redirect to the external login provider.
            //////var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            //////var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            //////return Challenge(properties, provider);

            return null;
        }

        //
        // GET: /Account/ExternalLoginCallback
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            return null;

            ////////if (remoteError != null)
            ////////{
            ////////    ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
            ////////    return View(nameof(Login));
            ////////}
            ////////var info = await _signInManager.GetExternalLoginInfoAsync();
            ////////if (info == null)
            ////////{
            ////////    return RedirectToAction(nameof(Login));
            ////////}

            ////////// Sign in the user with this external login provider if the user already has a login.
            ////////var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            ////////if (result.Succeeded)
            ////////{
            ////////    _logger.LogInformation(5, "User logged in with {Name} provider.", info.LoginProvider);
            ////////    return RedirectToLocal(returnUrl);
            ////////}
            ////////if (result.RequiresTwoFactor)
            ////////{
            ////////    return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl });
            ////////}
            ////////if (result.IsLockedOut)
            ////////{
            ////////    return View("Lockout");
            ////////}
            ////////else
            ////////{
            ////////    // If the user does not have an account, then ask the user to create an account.
            ////////    ViewData["ReturnUrl"] = returnUrl;
            ////////    ViewData["LoginProvider"] = info.LoginProvider;
            ////////    var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            ////////    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = email });
            ////////}
        }

 
        // GET: /Account/ConfirmEmail
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            ////////if (userId == null || code == null)
            ////////{
            ////////    return View("Error");
            ////////}
            ////////var user = await _userManager.FindByIdAsync(userId);
            ////////if (user == null)
            ////////{
            ////////    return View("Error");
            ////////}
            ////////var result = await _userManager.ConfirmEmailAsync(user, code);
            ////////return View(result.Succeeded ? "ConfirmEmail" : "Error");
            return null;
        }

        //
        // GET: /Account/ForgotPassword
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult ForgotPassword(UsuContraseñaViewModel oUsuCtrVM)
        {
            if (ModelState.IsValid)
            {
                string sArchivoPath = _app.ContentRootPath + "\\" + ConstantesWeb.Carpetas.PLANTILLAS  + "\\OlvidarContraseña.html";
                string sMensaje = "";

                if (System.IO.File.Exists(sArchivoPath))
                    sMensaje = System.IO.File.ReadAllText(sArchivoPath);
                else
                    sMensaje = "<html><head><meta http-equiv = 'Content- ype' content = 'text/html; charset=utf-8'></head><body><div><p>Estimado usuario</p><br/><p>Su contraseña ha sido modificada,</p><p>Favor de utilizar la siquiente cadena de caracteres como su nueva contraseña: | contraseña |</p><br/><p> Atentamente </p><p>Sistema de Transparencia de la SPF </p></div></body></html> ";

                Dictionary < string, object> dicParam = new Dictionary<string, object>();
                dicParam.Add(DButil.SIT_ADM_USUARIO_COL.USRCORREO, oUsuCtrVM.correo);
                dicParam.Add(SeguridadSer.PARAM_ARCHIVO_MENSAJE, sMensaje);
                dicParam.Add(SeguridadSer.PARAM_CFGCORREO, _memCache.ObtenerDato(CacheWebSIT.APP_CORREO_CONFIG) as CfgCorreoMdl);
                
                Boolean sRes = (Boolean )_sitDmlDbSer.operEjecutar<SeguridadSer>(nameof(SeguridadSer.OlvidarContraseña), dicParam);                
                if (sRes == true)
                    return View("ForgotPasswordConfirmation");               
                else
                    return View(oUsuCtrVM);
            }
            else
                return View(oUsuCtrVM);
        }


        //
        // GET: /Account/ForgotPasswordConfirmation
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
  

        //
        // GET: /Account/SendCode
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl = null, bool rememberMe = false)
        {
            ////////var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            ////////if (user == null)
            ////////{
            ////////    return View("Error");
            ////////}
            ////////var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(user);
            ////////var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            ////////return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
            return null;
        }
   
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }


        [HttpGet]
        public String SesionActiva()
        {
            return "Sesion";
        }

    }
}
