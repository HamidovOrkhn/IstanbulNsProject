using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IstanbulNsApp.Filters;
using IstanbulNsApp.Libs;
using IstanbulNsApp.Repositories;
using IstanbulNsApp.Resources.DTO;
using IstanbulNsApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;

namespace IstanbulNsApp.Controllers
{
    [Validate]
    public class OnlineTurnController : Controller
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _fc;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public OnlineTurnController(IConfiguration config, IHttpClientFactory fc, IStringLocalizer<SharedResource> localizer)
        {
            _config = config;
            _fc = fc.CreateClient(name: "ApiRequests");
            _config = config;
            _localizer = localizer;
        }
        public async Task<IActionResult> Index([FromQuery] int doctor)
        {
            int langId = 1;

            if (Request.Cookies["LangKey"] != null)
            {
                langId = Convert.ToInt32(Request.Cookies["LangKey"]);
            }
          
            var services = await new ServiceNodeAsync<object, OnlineTurnDTO>(_fc).GetClientAsync("/api/online_query/info/" + doctor+"/"+langId);
            var model = new OnlineTurnViewModel();
            model.OnlineData = services.Data;
            return View(model);
        }
    
        public async Task<IActionResult> Confirm([FromForm] OnlineTurnViewModel request)
        {
            #region ConfirmQuery
            OnlineQueryDTO resources = new OnlineQueryDTO();
            string code = (new Random().Next(1000, 999999999)).ToString();
            resources.Code = code;
            resources.DoctorId = request.DoctorId;
            resources.ServiceId = request.ServiceId;
            string parsedTime = request.Date + " " + request.Hour;
            DateTime time = Convert.ToDateTime(parsedTime);
            resources.QueryDate = time;
            resources.ServeDate = DateTime.Now;
            resources.PhoneNumber = request.PhoneNumber;
            resources.Email = request.Email;
            resources.Info = request.Info;
            resources.Subject = request.Subject;
            var resp = await new ServiceNodeAsync<OnlineQueryDTO, object>(_localizer, _fc).PostClientAsync(resources, "/api/online_query/confirm");
            if (resp.IsCatched == 1)
            {
                TempData["ServerResponseError"] = resp.Message.ToString();
                return RedirectToAction("Index","OnlineTurn",new {doctor = request.DoctorId});
            }
            #endregion
            TempData["ServerResponseSuccess"] = code;
            return RedirectToAction("Index","Home");
        }
        public IActionResult Result()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetResult([FromForm] ClientCredentials request)
        {
            ClientCredentialsDTO resources = new ClientCredentialsDTO();
            resources.PhoneNumber = request.PhoneNumber;
            resources.QueryId = request.QueryId.ToString();
            var resp =  new ServiceNode<ClientCredentialsDTO, Pdf>(_localizer, _fc).PostClient(resources, "/api/online_query/result");
            if (resp.IsCatched == 1)
            {
                TempData["ServerResponseErrorQ"] = resp.Message.ToString();
                return RedirectToAction("Result", "OnlineTurn");
            }
            if (resp.Data is object)
            {
                TempData["ServerResponseSuccessQ"] = resp.Data.FileName;
            }
            else
            {
                TempData["ServerResponseErrorQ"] = _localizer["Sizin sorğunuzun cavabı hələ hazır deyil"].ToString();
            }
          
            return RedirectToAction("Result", "OnlineTurn");
        }
    }
}
