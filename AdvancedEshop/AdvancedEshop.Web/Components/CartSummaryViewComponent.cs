using AdvancedEshop.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedEshop.Web.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private Cart cart;
        public CartSummaryViewComponent(Cart cartService)
        {
            cart = cartService;
        }
        public IViewComponentResult Invoke()
        {
            return View(cart);
        }
    }

}
