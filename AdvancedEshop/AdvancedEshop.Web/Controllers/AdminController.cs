// AdminController.cs
using AdvancedEshop.Web.API.Models;
using AdvancedEshop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvancedEshop.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IHttpClientFactory _clientFactory;

        public AdminController(IOrderRepository orderRepository, IHttpClientFactory clientFactory)
        {
            _orderRepository = orderRepository;
            _clientFactory = clientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var allOrders = await _orderRepository.Orders.ToListAsync();
            var unshippedOrders = allOrders.Where(o => !o.Shipped);
            var shippedOrders = allOrders.Where(o => o.Shipped);

            var viewModel = new AdminOrdersViewModel
            {
                AllOrders = allOrders,
                UnshippedOrders = unshippedOrders,
                ShippedOrders = shippedOrders
            };

            return View(viewModel);
        }

        public IActionResult ShipOrder(int id)
        {
            UpdateOrder(id, true);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ResetOrder(int id)
        {
            UpdateOrder(id, false);
            return RedirectToAction(nameof(Index));
        }

        private void UpdateOrder(int id, bool shipValue)
        {
            Order? o = _orderRepository.Orders.FirstOrDefault(o => o.OrderID == id);
            if (o != null)
            {
                o.Shipped = shipValue;
                _orderRepository.SaveOrder(o);
            }
        }

        public async Task<IActionResult> ProductAll()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7136/products2");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<Product>>(content);

            return View("ProductAll", products);
        }

        public async Task<IActionResult> ProductDetails(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7136/products2/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(content);

            return View("ProductDetails", product);
        }

        public IActionResult CreateProduct()
        {
            // Tạo một ViewModel mới nếu bạn cần truyền dữ liệu khác đến trang tạo mới
            return View("CreateProduct");
        }

        public async Task<IActionResult> SaveProductAsync(Product product)
        {
            // Xử lý lưu thông tin sản phẩm vào cơ sở dữ liệu
            // (sử dụng _orderRepository hoặc DbContext tùy thuộc vào cách bạn đang lưu trữ dữ liệu)

            // Sau khi lưu, chuyển hướng về trang danh sách sản phẩm hoặc trang chi tiết sản phẩm
            // Tùy thuộc vào yêu cầu của bạn
            if (ModelState.IsValid)
            {
                using var client = _clientFactory.CreateClient();

                var response = await client.PostAsJsonAsync("https://localhost:7136/products2", product);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(ProductAll));
                }
                else
                {
                    // Xử lý lỗi nếu có
                    ModelState.AddModelError(string.Empty, "Error creating product. Please try again.");
                }
            }

            /*return View(product);*/
            return RedirectToAction(nameof(ProductAll));
        }

        /*public async Task<IActionResult> EditProduct(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7136/products2/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(content);

            return View("EditProduct", product);
        }*/

        public async Task<IActionResult> EditProduct(int id)
        {
            var client = _clientFactory.CreateClient();

            // Lấy thông tin sản phẩm
            var productResponse = await client.GetAsync($"https://localhost:7136/products2/{id}");
            productResponse.EnsureSuccessStatusCode();
            var productContent = await productResponse.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(productContent);

            // Lấy danh sách categories
            var categoriesResponse = await client.GetAsync("https://localhost:7136/categories");
            categoriesResponse.EnsureSuccessStatusCode();
            var categoriesContent = await categoriesResponse.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<Category>>(categoriesContent);

            // Gán danh sách categories vào ViewBag để sử dụng trong view
            ViewBag.Categories = categories;

            return View("EditProduct", product);
        }


        // AdminController.cs
        /*[HttpPost]*/
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                using var client = _clientFactory.CreateClient();

                // Chú ý: Cần kiểm tra xem product.ProductId đã được đặt giá trị hay chưa.
                if (product.ProductId == 0)
                {
                    ModelState.AddModelError(string.Empty, "Invalid product ID.");
                    return View("EditProduct", product);
                }

                var response = await client.PutAsJsonAsync($"https://localhost:7136/products2/{product.ProductId}", product);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(ProductAll));
                }
                else
                {
                    // Xử lý lỗi nếu có
                    ModelState.AddModelError(string.Empty, "Error updating product. Please try again.");
                }
            }

            // Nếu ModelState không hợp lệ, quay lại trang chỉnh sửa với dữ liệu người dùng đã nhập
            return View("EditProduct", product);
        }

    }
}
