using AdvancedEshop.Web.API.Models;
using AdvancedEshop.Web.Infrastructure;
using AdvancedEshop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AdvancedEshop.Web.Components
{
    public class CartWidget : ViewComponent
    {
        

        public IViewComponentResult Invoke()
        {
            return View(HttpContext.Session.GetJson<Cart>("cart"));
        }
    }
}
