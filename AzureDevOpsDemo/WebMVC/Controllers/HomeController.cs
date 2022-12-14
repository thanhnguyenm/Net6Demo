using DomainEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<HomeController> _logger;
        private readonly ITokenAcquisition _tokenAcquisition;

        public HomeController(
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            ILogger<HomeController> logger,
            ITokenAcquisition tokenAcquisition)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _tokenAcquisition = tokenAcquisition;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            //call secured api with on-behalf-flow
            //https://github.com/Azure-Samples/active-directory-aspnetcore-webapp-openidconnect-v2/blob/master/4-WebApp-your-API/4-2-B2C/

            using (var httpClient = _httpClientFactory.CreateClient())
            using (var request = new HttpRequestMessage(HttpMethod.Get, _configuration.GetValue<string>("DownstreamApi:BaseUrl") + "weatherforecast"))
            {
                await PrepareAuthenticatedClient(httpClient);
                var result = await SendRequestAsync<IEnumerable<WeatherForecast>>(httpClient, request, cancellationToken);

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

        private async Task PrepareAuthenticatedClient(HttpClient client)
        {
            var scopes = _configuration.GetValue<string>("DownstreamApi:Scopes").Split(' ');

            var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(scopes);
            Debug.WriteLine($"access token-{accessToken}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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