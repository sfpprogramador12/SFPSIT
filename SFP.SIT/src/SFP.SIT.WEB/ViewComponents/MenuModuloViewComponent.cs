using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SFP.SIT.SERV.Model.ADM;
using SFP.SIT.WEB.Models;
using SFP.SIT.WEB.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFP.SIT.WEB.ViewsComponents
{
    [ViewComponent(Name = "MenuModulo")]
    public class MenuModuloViewComponent : ViewComponent
    {
        IHttpContextAccessor _ContextAccessor;

        public MenuModuloViewComponent(IHttpContextAccessor httpContextAccessor) 
        {
            _ContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {                                    
            return View(await Task.Run(() => ObtenerMenu()) );
        }

        private MenuViewModel ObtenerMenu()
        {
            MenuViewModel _baseViewMdl = new MenuViewModel();
            _baseViewMdl.lstAdmModMdl = JsonConvert.DeserializeObject<List<SIT_ADM_MODULO>>(
                _ContextAccessor.HttpContext.User.FindFirst(ConstantesWeb.Usuario.MODULOS).Value);

            return _baseViewMdl;
        }


    }
}
