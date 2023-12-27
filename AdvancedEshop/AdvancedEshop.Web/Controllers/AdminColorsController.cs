// Controllers/AdminColorsController.cs
using AdvancedEshop.Web.API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AdvancedEshop.Web.Controllers
{
    public class AdminColorsController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public AdminColorsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> ColorAll()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7136/colors2");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var colors = JsonConvert.DeserializeObject<List<Color>>(content);

            return View("ColorAll", colors);
        }

        public IActionResult CreateColor()
        {
            return View("CreateColor");
        }

        [HttpPost]
        public async Task<IActionResult> SaveColor(Color color)
        {
            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient();
                var response = await client.PostAsJsonAsync("https://localhost:7136/colors2", color);
                response.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(ColorAll));
            }

            return View("SaveColor", color);
        }

        [HttpGet]
        public async Task<IActionResult> ColorDetails(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7136/colors2/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var color = JsonConvert.DeserializeObject<Color>(content);

            return View("ColorDetails", color);
        }

        [HttpGet]
        public async Task<IActionResult> EditColor(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7136/colors2/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var color = JsonConvert.DeserializeObject<Color>(content);

            return View("EditColor", color);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateColor(Color color)
        {
            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient();
                var response = await client.PutAsJsonAsync($"https://localhost:7136/colors2/{color.ColorId}", color);
                response.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(ColorAll));
            }

            return View("EditColor", color);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteColor(int id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.DeleteAsync($"https://localhost:7136/colors2/{id}");
            response.EnsureSuccessStatusCode();

            return RedirectToAction(nameof(ColorAll));
        }
    }
}
