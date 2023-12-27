using AdvancedEshop.Web.API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdvancedEshop.Web.Controllers
{
    public class AdminCategoriesController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public AdminCategoriesController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> CategoryAll()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7136/categories2");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<Category>>(content);

            return View("CategoryAll", categories);
        }

        public IActionResult CreateCategory()
        {
            // Hiển thị form tạo mới Category
            return View("CreateCategory");
        }

        /*[HttpPost]*/
        public async Task<IActionResult> SaveCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient();
                var response = await client.PostAsJsonAsync("https://localhost:7136/categories2", category);
                response.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(CategoryAll));
            }

            // Nếu ModelState không hợp lệ, quay lại trang tạo mới Category
            return View("CreateCategory", category);
        }

        public async Task<IActionResult> CategoryDetails(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7136/categories2/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var category = JsonConvert.DeserializeObject<Category>(content);

            return View("CategoryDetails", category);
        }

        public async Task<IActionResult> EditCategory(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7136/categories2/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var category = JsonConvert.DeserializeObject<Category>(content);

            return View("EditCategory", category);
        }

       /* [HttpPost]*/
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient();
                var response = await client.PutAsJsonAsync($"https://localhost:7136/categories2/{category.CategoryId}", category);
                response.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(CategoryAll));
            }

            // Nếu ModelState không hợp lệ, quay lại trang chỉnh sửa Category
            return View("EditCategory", category);
        }

        /*[HttpPost]*/
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7136/categories2/removeconfirmed/{id}");
            response.EnsureSuccessStatusCode();

            return RedirectToAction(nameof(CategoryAll));
        }
    }
}
