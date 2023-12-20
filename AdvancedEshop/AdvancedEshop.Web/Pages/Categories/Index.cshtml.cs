using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AdvancedEshop.Web.API.Models;

namespace AdvancedEshop.Web.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public List<Category>? Categories { get; set; }

        public IndexModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task OnGetAsync()
        {
            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:7136/categories");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            Categories = JsonConvert.DeserializeObject<List<Category>>(content);
        }
    }
}
