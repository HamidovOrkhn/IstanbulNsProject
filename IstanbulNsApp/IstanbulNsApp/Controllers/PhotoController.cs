using IstanbulNsApp.Libs;
using IstanbulNsApp.Models;
using IstanbulNsApp.Repositories;
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
    public class PhotoController : Controller
    {
        private readonly ILogger<PhotoController> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient _fc;
        public PhotoController(ILogger<PhotoController> logger, IConfiguration config, IHttpClientFactory fc)
        {
            _logger = logger;
            _config = config;
            _fc = fc.CreateClient(name: "ApiRequests");
            _config = config;
        }
        public async Task<IActionResult> Index()
        {
            #region AboutUsData
            var model = await new ServiceNodeAsync<List<Photo>, List<Photo>>(_fc)
                    .GetClientAsync("/api/photo/list/");

            return View(model.Data);
            #endregion

        }

    }
}
