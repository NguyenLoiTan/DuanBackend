using AdvancedEshop.Web.API.Models;
using AdvancedEshop.Web.Infrastructure;
using AdvancedEshop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;

namespace AdvancedEshop.Web.Pages
{
    public class CartModel : PageModel
    {
        private IOrderRepository repository;
        private readonly IHttpClientFactory _clientFactory;
        public CartModel(IOrderRepository repo, Cart cartService, IHttpClientFactory clientFactory)
        {
            repository = repo;
            Cart = cartService;
            _clientFactory = clientFactory;
        }
       

        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; } = "/";
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            //Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }

        /*public async Task<IActionResult> Index()
        {
            return RedirectToPage("/Cart", HttpContext.Session.GetJson<Cart>("cart"));
        }*/



        public IActionResult OnPost(long productId, string returnUrl)
        {
            Product? product = repository.Products
            .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                Cart.AddItem(product, 1);
            }
            return RedirectToPage(new { returnUrl = returnUrl });
        }
        public IActionResult OnPostRemove(long productId, string returnUrl)
        {
            Cart.RemoveLine(Cart.Lines.First(cl =>
            cl.Product.ProductId == productId).Product);
            return RedirectToPage(new { returnUrl = returnUrl });
        }




        public IActionResult OnPostAdd(long productId, string returnUrl)
        {
            Product? product = repository.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                Cart.AddItem(product, 1);
                // Thêm log hoặc điểm chèn để kiểm tra
                Console.WriteLine($"Added product {productId} to the cart.");
            }
            return RedirectToPage(new { returnUrl = returnUrl });
        }

        public IActionResult OnPostGiam(long productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {
                Cart.GiamItem(product, 1);
            }

            return RedirectToPage(new { returnUrl = returnUrl });
        }







        /*public async Task<IActionResult> OnPostAddToCartAsync(int id)
        {
            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync($"https://localhost:7136/products2/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(content);

            // Cập nhật giỏ hàng
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            Cart.AddItem(product, 1);
            HttpContext.Session.SetJson("cart", Cart);

            // Chuyển hướng sang view "Cart" với thông tin giỏ hàng
            return RedirectToPage("/Cart", new { returnUrl = ReturnUrl });
        }

        public async Task<IActionResult> OnPostUpdateCartAsync(int id)
        {
            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync($"https://localhost:7136/products2/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(content);

            // Cập nhật giỏ hàng
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            Cart.UpdateLine(product, 1); // Tăng số lượng lên 1
            HttpContext.Session.SetJson("cart", Cart);

            // Chuyển hướng sang trang "Cart" với thông tin giỏ hàng
            return RedirectToPage("/Cart", Cart);
        }*/


    }


}
