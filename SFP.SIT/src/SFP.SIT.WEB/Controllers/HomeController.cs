using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace SFP.SIT.WEB.Controllers
{
    public class HomeController : Controller
    {
        IHostingEnvironment _app;

        public HomeController(IHostingEnvironment app)
        {
            _app = app;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            ViewData["Message"] = "Acerca del sistema";

            return View();
        }

        [AllowAnonymous]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Pagina de contacto";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public FileResult Manual()
        {
            HttpContext.Response.ContentType = "application/pdf";
            FileContentResult result = null;

            string sRuta = _app.ContentRootPath + "\\Manual\\ManualUsuarioSIT.pdf";
            

            if (System.IO.File.Exists(sRuta) )
            {
                result = new FileContentResult(System.IO.File.ReadAllBytes(sRuta), "application/pdf");
            }

            return result;
        }

    }
}