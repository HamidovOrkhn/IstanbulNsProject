using IstanbulNsApp.Libs;
using IstanbulNsApp.Models;
using IstanbulNsApp.Repositories;
using IstanbulNsApp.ViewModels;
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
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient _fc;
        public BlogController(ILogger<BlogController> logger, IConfiguration config, IHttpClientFactory fc)
        {
            _logger = logger;
            _config = config;
            _fc = fc.CreateClient(name: "ApiRequests");
            _config = config;
        }
        public async Task<IActionResult> Index()
        {
            #region BlogData
            int langId = 1;

            if (Request.Cookies["LangKey"] != null)
            {
                langId = Convert.ToInt32(Request.Cookies["LangKey"]);
            }

            var model = await new ServiceNodeAsync<object, List<Blog>>(_fc)
                   .GetClientAsync("/api/blog/" + langId);

            #endregion
            return View(model.Data);
        }
        public async Task<IActionResult> Single(int id)
        {
            #region BlogData
            int langId = 1;

            if (Request.Cookies["LangKey"] != null)
            {
                langId = Convert.ToInt32(Request.Cookies["LangKey"]);
            }
            ReturnMessage<BlogViewModel> resp = await new ServiceNodeAsync<object,BlogViewModel>(_fc)
           .GetClientAsync("/api/blog/single/" + id);


            #endregion
            return View(resp);
        }
    }
}
