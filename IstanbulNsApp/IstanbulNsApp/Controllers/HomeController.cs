using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IstanbulNsApp.Models;
using IstanbulNsApp.Libs;
using Microsoft.Extensions.Configuration;
using IstanbulNsApp.Resources.DTO;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace IstanbulNsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient _fc;
        public HomeController(ILogger<HomeController> logger, IConfiguration config, IHttpClientFactory fc)
        {
            _logger = logger;
            _config = config;
            _fc = fc.CreateClient(name: "ApiRequests");
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet("/test")]
        public async Task<IActionResult> Get()
        {
           var services = await new ServiceNodeAsync<object, List<ServiceDTO>>(_fc).GetClientAsync("/api/services/services/1");
            return Ok(services);
        }

        [HttpGet]
        public async Task<IActionResult> SetLanguage([FromQuery] string Culture, [FromQuery] string ReturnUrl)
        {
            #region SetLangCookie
            var resp = await new ServiceNodeAsync<object, LangDTO>(_fc).GetClientAsync("/api/lang/get/" + Culture);
            Response.Cookies.Append("LangKey", resp.Data.Id.ToString(),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1) });
            Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(resp.Data.Key)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1) }
        );
            #endregion
            return LocalRedirect(ReturnUrl);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
