using IstanbulNsApp.Libs;
using IstanbulNsApp.Resources.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IstanbulNsApp.ViewComponents
{
    public class LanguageViewComponent:ViewComponent
    {
        private readonly HttpClient _fc;
        public LanguageViewComponent(IHttpClientFactory fc)
        {
            _fc = fc.CreateClient(name: "ApiRequests");
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = new ServiceNodeAsync<object, List<LangDTO>>(_fc);
            var services = await client.GetClientAsync("/api/lang/all");
            return View(services.Data);
        }
    }
}
