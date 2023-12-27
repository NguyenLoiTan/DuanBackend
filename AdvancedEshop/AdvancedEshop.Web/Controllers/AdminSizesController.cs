using AdvancedEshop.Web.API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdvancedEshop.Web.Controllers
{
    public class AdminSizesController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public AdminSizesController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> SizeAll()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7136/sizes2");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var sizes = JsonConvert.DeserializeObject<List<Size>>(content);

            return View("SizeAll", sizes);
        }

        public IActionResult CreateSize()
        {
            // Thêm logic để hiển thị form tạo mới Size (nếu cần)
            return View("CreateSize");
        }

        [HttpPost]
        public async Task<IActionResult> SaveSize(Size size)
        {
            // Thêm logic để lưu Size (gửi POST request tới API, nếu cần)
            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient();
                var response = await client.PostAsJsonAsync("https://localhost:7136/sizes2", size);
                response.EnsureSuccessStatusCode();

                // Redirect về trang danh sách Size sau khi lưu
                return RedirectToAction(nameof(SizeAll));
            }

            // Nếu ModelState không hợp lệ, quay lại trang tạo mới Size
            return View("SaveSize", size);
        }

        public async Task<IActionResult> SizeDetails(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7136/sizes2/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var size = JsonConvert.DeserializeObject<Size>(content);

            if (size != null)
            {
                return View("SizeDetails", size);
            }

            return NotFound();
        }

        public async Task<IActionResult> EditSize(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7136/sizes2/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var size = JsonConvert.DeserializeObject<Size>(content);

            if (size != null)
            {
                return View("EditSize", size);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSize(Size size)
        {
            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient();
                var response = await client.PutAsJsonAsync($"https://localhost:7136/sizes2/{size.SizeId}", size);
                response.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(SizeAll));
            }

            return View("EditSize", size);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSize(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7136/sizes2/{id}");
            response.EnsureSuccessStatusCode();

            return RedirectToAction(nameof(SizeAll));
        }

    }
}
