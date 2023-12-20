using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using AdvancedEshop.Web.API.Models;

namespace AdvancedEshop.Web.Components
{
    public class Trandy : ViewComponent
    {
        private readonly IHttpClientFactory _clientFactory;

        public Trandy(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IViewComponentResult Invoke()
        {
            try
            {
                var client = _clientFactory.CreateClient();

                // Gọi API để lấy danh sách sản phẩm
                var response = client.GetAsync("https://localhost:7136/products").Result;
                response.EnsureSuccessStatusCode();

                var content = response.Content.ReadAsStringAsync().Result;
                var allProducts = JsonConvert.DeserializeObject<List<Product>>(content);

                // Kiểm tra allProducts có giá trị null hay không
                if (allProducts != null)
                {
                    // Lọc danh sách sản phẩm theo điều kiện IsTrandy
                    var trandyProducts = allProducts.Where(p => p.IsTrandy == true).ToList();

                    return View(trandyProducts);
                }
                else
                {
                    // Đặt thông báo lỗi vào ViewBag để sử dụng trong View
                    return View(allProducts);
                }
            }
            catch (Exception)
            {
                // Đặt thông báo lỗi vào ViewBag để sử dụng trong View
                ViewBag.ErrorMessage = "There was an error fetching products. Please try again later.";
                return View(new List<Product>());
            }
        }
    }
}
