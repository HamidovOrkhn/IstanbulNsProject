using IstanbulNsApp.Libs;
using IstanbulNsApp.Resources.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IstanbulNsApp.ViewComponents
{
    public class NavServiceViewComponent:ViewComponent
    {
        private readonly HttpClient _fc;
        private readonly IConfiguration _config;
        public NavServiceViewComponent(IHttpClientFactory fc, IConfiguration config)
        {
            _fc = fc.CreateClient(name: "ApiRequests");
            _config = config;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            int langId = 1;

            if (Request.Cookies["LangKey"] != null)
            {
                langId = Convert.ToInt32(Request.Cookies["LangKey"]);
            }
            var client = new ServiceNodeAsync<object, List<ServiceDTO>>(_fc);
            var services = await client.GetClientAsync("/api/services/services/" + langId);
            return View(services.Data.OrderBy(a => a.Id).Take(6));
        }
    }
}
