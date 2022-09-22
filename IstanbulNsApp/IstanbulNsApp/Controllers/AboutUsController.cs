using IstanbulNsApp.Libs;
using IstanbulNsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IstanbulNsApp.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly ILogger<AboutUsController> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient _fc;
        public AboutUsController(ILogger<AboutUsController> logger, IConfiguration config, IHttpClientFactory fc)
        {
            _logger = logger;
            _config = config;
            _fc = fc.CreateClient(name: "ApiRequests");
            _config = config;
        }
        public async Task<IActionResult>  Index()
        {
            #region AboutUsData
            int langId = 1;

            if (Request.Cookies["LangKey"] != null)
            {
                langId = Convert.ToInt32(Request.Cookies["LangKey"]);
            }

            var model = await new ServiceNodeAsync<object, AboutUs>(_fc)
                   .GetClientAsync("/api/aboutus/" + langId);

            #endregion
            return View(model.Data);
        }
    }
}
