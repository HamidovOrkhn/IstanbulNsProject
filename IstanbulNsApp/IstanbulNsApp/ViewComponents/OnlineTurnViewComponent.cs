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
    public class OnlineTurnViewComponent : ViewComponent
    {
        private readonly HttpClient _fc;
        private readonly IConfiguration _config;
        public OnlineTurnViewComponent(IHttpClientFactory fc, IConfiguration config)
        {
            _fc = fc.CreateClient(name: "ApiRequests");
            _config = config;

        }
        public async Task<IViewComponentResult> InvokeAsync(int doctorId = 0)
        {
            var services = await new ServiceNodeAsync<object, OnlineTurnDTO>(_fc).GetClientAsync("/api/online_query/info/" + doctorId);
            return View(services.Data);
        }

    }
}
