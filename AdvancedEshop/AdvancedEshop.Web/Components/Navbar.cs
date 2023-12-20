using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using AdvancedEshop.Web.API.Models;

namespace AdvancedEshop.Web.Components
{
    public class Navbar : ViewComponent
    {
        private readonly IHttpClientFactory _clientFactory;

        public Navbar(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IViewComponentResult Invoke()
        {
            var client = _clientFactory.CreateClient();

            var response = client.GetAsync("https://localhost:7136/categories").Result;
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            var categories = JsonConvert.DeserializeObject<List<Category>>(content);

            return View(categories);
        }
    }


}



