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
    public class VideoController : Controller
    {
        private readonly ILogger<VideoController> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient _fc;
        public VideoController(ILogger<VideoController> logger, IConfiguration config, IHttpClientFactory fc)
        {
            _logger = logger;
            _config = config;
            _fc = fc.CreateClient(name: "ApiRequests");
            _config = config;
        }
        public async Task<IActionResult> Index()
        {
            #region AboutUsData
            var model = await new ServiceNodeAsync<List<Video>, List<Video>>(_fc)
                    .GetClientAsync("/api/video/list/");

            return View(model.Data);
            #endregion

        }
    }
}
