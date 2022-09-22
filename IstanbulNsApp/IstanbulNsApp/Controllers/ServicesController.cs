using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IstanbulNsApp.Libs;
using IstanbulNsApp.Resources.DTO;
using IstanbulNsApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace IstanbulNsApp.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient _fc;
        public ServicesController(ILogger<HomeController> logger, IConfiguration config, IHttpClientFactory fc)
        {
            _logger = logger;
            _config = config;
            _fc = fc.CreateClient(name: "ApiRequests");
            _config = config;
        }
        public async Task<IActionResult> Index([FromQuery] int serve)
        {
            int langId = 1;
            int serviceId = 1;
            if (serve!=null)
            {
                serviceId = serve;
            }
            if (Request.Cookies["LangKey"] != null)
            {
                langId = Convert.ToInt32(Request.Cookies["LangKey"]);
            }
            var modal = await new ServiceNodeAsync<object, ServiceInfo>(_fc).GetClientAsync("/api/services/info/"+langId+"/"+serviceId);
            return View(modal.Data);
        }
        public async Task<IActionResult> GetService([FromQuery] int serve)
        {
            int langId = 1;
            int serviceId = 1;
            if (serve > 0)
            {
                serviceId = serve;
            }
            if (Request.Cookies["LangKey"] != null)
            {
                langId = Convert.ToInt32(Request.Cookies["LangKey"]);
            }
            var modal = await new ServiceNodeAsync<object, ServiceInfo>(_fc).GetClientAsync("/api/services/info/" + langId + "/" + serviceId);
            return Ok(modal.Data);
        }
        public async Task<IActionResult> AllServices()
        {
            int langId = 1;
            if (Request.Cookies["LangKey"] != null)
            {
                langId = Convert.ToInt32(Request.Cookies["LangKey"]);
            }
            var modal = await new ServiceNodeAsync<object, List<ServiceInfoMini>>(_fc).GetClientAsync("/api/services/all/" + langId);
            return View(modal.Data);
        }
        public async Task<IActionResult> Doctors([FromQuery] int page)
        {
            int pagecount = 0;
            if (page is object)
            {
                pagecount = page;
            }
            var services = await new ServiceNodeAsync<object, DoctorViewModel>(_fc).GetClientAsync("/api/doctors/all/" + pagecount);
            return View(services.Data);

        }
        public async Task<IActionResult> DoctorsPartial([FromQuery] int page)
        {
            int pagecount = 0;
            if (page is object)
            {
                pagecount = page;
            }
            var services = await new ServiceNodeAsync<object, DoctorViewModel>(_fc).GetClientAsync("/api/doctors/all/" + pagecount);
            return View(services.Data);

        }
        public async Task<IActionResult> DoctorsInfo([FromQuery] int doctor)
        {
            int doctorId = 0;
            if (doctor > 0)
            {
                doctorId = doctor;
            }
            int langId = 1;

            if (Request.Cookies["LangKey"] != null)
            {
                langId = Convert.ToInt32(Request.Cookies["LangKey"]);
            }
            var services = new ServiceNode<object, DoctorInfoDTO>(_fc).GetClient("/api/doctors/info/" + doctorId+"/"+langId);
            return View(services.Data);

        }
        public async Task<IActionResult> DoctorFind([FromQuery] string st)
        {
            string first = string.Empty;
            if (st is object)
            {
                first = st;
            }
            var services = await new ServiceNodeAsync<object, DoctorViewModel>(_fc).GetClientAsync("/api/doctors/find/" + first);
            return View("Doctors",services.Data);

        }



    }
}
