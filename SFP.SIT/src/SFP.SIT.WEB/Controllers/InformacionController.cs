using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFP.SIT.WEB.Injection;
using SFP.SIT.WEB.Models;

namespace SFP.SIT.WEB.Controllers
{
    public class InformacionController : SitBaseCtlr
    {
        public InformacionController(ICacheWebSIT memCache, IHttpContextAccessor httpContextAccessor, ILogger<InformacionController> logger, IHostingEnvironment app)
            : base(memCache, httpContextAccessor, logger, app)
        {

        }
    
        [HttpGet]
        public IActionResult Plazos()
        {
            //////ViewBag.Nombre = "Makdihel - MLS";

            return View();

        }
    }
}
