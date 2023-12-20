using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AdvancedEshop.Web.API.Models;
using Newtonsoft.Json;

namespace AdvancedEshop.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }


        // GET: /Product/Index/5
        public async Task<IActionResult> Index(int id)
        {
            
            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync($"https://localhost:7136/products?categoryId={id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<Product>>(content);


            var filteredProducts = products?.Where(p => p.Category?.CategoryId == id || p.Category == null).ToList();

            return View(filteredProducts);
        }



        // Load dữ liệu từ API hoặc nơi khác và truyền nó cho view
        /*List<Product> products = LoadProductsFromApiOrOtherSource();
        return View(products);*/
        // GET: /Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync($"https://localhost:7136/products/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(content);
            return View(product);
        }


        // GET: /Product/Create
        public IActionResult Create()
        {
            // Trả về view để tạo sản phẩm mới
            return View();
        }

        // POST: /Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            // Xử lý logic để tạo mới sản phẩm, có thể gọi API hoặc thực hiện các thao tác khác
            if (ModelState.IsValid)
            {
                // Gọi API hoặc xử lý tạo mới sản phẩm ở đây
                // Sau đó chuyển hướng đến trang danh sách sản phẩm hoặc trang chi tiết sản phẩm mới tạo
                return RedirectToAction(nameof(Index));
            }

            // Nếu có lỗi hợp lệ, trở lại view để người dùng sửa
            return View(product);
        }

        // GET: /Product/Edit/5
        public IActionResult Edit(int id)
        {
            // Load thông tin sản phẩm từ API hoặc nơi khác và truyền cho view để chỉnh sửa
            Product product = LoadProductDetailsFromApiOrOtherSource(id);
            return View(product);
        }

        // POST: /Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            // Xử lý logic để chỉnh sửa sản phẩm, có thể gọi API hoặc thực hiện các thao tác khác
            if (ModelState.IsValid)
            {
                // Gọi API hoặc xử lý chỉnh sửa sản phẩm ở đây
                // Sau đó chuyển hướng đến trang danh sách sản phẩm hoặc trang chi tiết sản phẩm đã chỉnh sửa
                return RedirectToAction(nameof(Index));
            }

            // Nếu có lỗi hợp lệ, trở lại view để người dùng sửa
            return View(product);
        }

        // GET: /Product/Delete/5
        public IActionResult Delete(int id)
        {
            // Load thông tin sản phẩm từ API hoặc nơi khác và truyền cho view để xác nhận xóa
            Product product = LoadProductDetailsFromApiOrOtherSource(id);
            return View(product);
        }

        // POST: /Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Xử lý logic để xóa sản phẩm, có thể gọi API hoặc thực hiện các thao tác khác
            // Sau đó chuyển hướng đến trang danh sách sản phẩm
            return RedirectToAction(nameof(Index));
        }

        private List<Product> LoadProductsFromApiOrOtherSource()
        {
            // Thực hiện logic để lấy danh sách sản phẩm từ API hoặc nơi khác
            // Trả về danh sách sản phẩm
            return new List<Product>();
        }

        private Product LoadProductDetailsFromApiOrOtherSource(int id)
        {
            // Thực hiện logic để lấy thông tin sản phẩm từ API hoặc nơi khác
            // Trả về thông tin sản phẩm
            return new Product();
        }
    }
}
