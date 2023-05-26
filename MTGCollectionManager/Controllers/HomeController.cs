using Microsoft.AspNetCore.Mvc;
using MTGCollectionManager.Models;
using MTGCollectionManager.Models.API;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;

namespace MTGCollectionManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;

            _httpClient = httpClientFactory.CreateClient("magicthegathering");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Index(string page)
        {
            string route = $"cards?page={page ?? "1"}";

            HttpResponseMessage response;

            do { response = await _httpClient.GetAsync(route); }

            while (!response.IsSuccessStatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var card = JsonSerializer.Deserialize<ResultsViewModel<MTGAPImodel>>(responseString);

            return View(card);
        }

        public async Task<IActionResult> CardAPI(string id)
        {
            var response = await _httpClient.GetAsync($"cards/{id}");

            if (id is null || response.IsSuccessStatusCode == false)
                return RedirectToAction(nameof(Index));

            var responseString = await response.Content.ReadAsStringAsync();
            var card = JsonSerializer.Deserialize<MTGAPImodel>(responseString);

            return View(card);
        }

        public async Task<IActionResult> Privacy(string page)
        {
            string route = $"cards?page={page ?? "1"}";

            HttpResponseMessage response;

            do { response = await _httpClient.GetAsync(route); }

            while (!response.IsSuccessStatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var card = JsonSerializer.Deserialize<ResultsViewModel<MTGAPImodel>>(responseString);

            return View(card);
        }
    }
}