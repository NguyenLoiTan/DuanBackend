using AdvancedEshop.Web.API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AdvancedEshop.Web.Components
{
    public class Imagebar:ViewComponent
    {
        private readonly IHttpClientFactory _clientFactory;

        public Imagebar(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IViewComponentResult Invoke()
        {
            var client = _clientFactory.CreateClient();

            var response = client.GetAsync("https://localhost:7136/categories2").Result;
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            var categories = JsonConvert.DeserializeObject<List<Category>>(content);

            return View("Index",categories);
        }
    }
}
