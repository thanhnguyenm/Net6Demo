using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            ILogger<HomeController> logger)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            //call secured api with on-behalf-flow
            //https://github.com/Azure-Samples/active-directory-aspnetcore-webapp-openidconnect-v2/blob/master/2-WebApp-graph-user/2-1-Call-MSGraph/README-incremental-instructions.md

            using (var httpClient = _httpClientFactory.CreateClient())
            using (var request = new HttpRequestMessage(HttpMethod.Get, _configuration.GetValue<string>("AzureAD:DownstreamApi:BaseUrl" + "weatherforecast")))
            {
                var result = await SendRequestAsync<string>(httpClient, request, cancellationToken);

                return View(result);
            }
        }

        private static async Task<T> SendRequestAsync<T>(HttpClient client, HttpRequestMessage request, CancellationToken cancellationToken) where T : class
        {
            using (var response = await client.SendAsync(request, cancellationToken))
            {
                var responseString = await response.Content.ReadAsStringAsync(cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<T>(responseString);
                    return result;
                }
                return null;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}