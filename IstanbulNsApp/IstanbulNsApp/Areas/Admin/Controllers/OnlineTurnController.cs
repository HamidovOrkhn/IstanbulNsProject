using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IstanbulNsApp.Areas.Admin.Models;
using IstanbulNsApp.Libs;
using IstanbulNsApp.Repositories;
using IstanbulNsApp.Resources.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System.Net.Http.Headers;
using RestSharp;
using IstanbulNsApp.ViewModels;
using IstanbulNsApp.Areas.Admin.ViewModels;
using IstanbulNsApp.Filters;
using static IstanbulNsApp.Resources.Enums.RoleEnum;

namespace IstanbulNsApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthUser((int)Roles.SubAdmin)]
    public class OnlineTurnController : Controller
    {
        private readonly HttpClient _fc;
        private readonly IConfiguration _configuration;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public OnlineTurnController(IHttpClientFactory fc, IConfiguration config, IStringLocalizer<SharedResource> localizer)
        {
            _fc = fc.CreateClient(name: "ApiRequests");
            _configuration = config;
            _localizer = localizer;
        }
        public async Task<IActionResult> OnlineTurnSection([FromQuery] int page = 1)
        {
            ReturnMessage<List<OnlineQuery>> sv = await new ServiceNodeAsync<object, List<OnlineQuery>>(_fc).GetClientAsync($"/api/online_query/getall/{page}");
            OnlineTurnVm model = new OnlineTurnVm();
            model.queries = sv.Data;
            return View(model);
        } 
        public IActionResult Disable([FromQuery]int query_id)
        {
            var client = new ServiceNode<object, object>(_fc).GetClient($"/api/online_query/disable/{query_id}");
            return RedirectToAction("OnlineTurnSection");
        }
        public IActionResult Enable([FromQuery] int query_id)
        {
            var client = new ServiceNode<object, object>(_fc).GetClient($"/api/online_query/enable/{query_id}");
            return RedirectToAction("OnlineTurnSection");
        }
        public IActionResult EndSession([FromQuery]int query_id)
        {
            var client = new ServiceNode<object, object>(_fc).GetClient($"/api/online_query/endsession/{query_id}");
            return RedirectToAction("OnlineTurnSection");
        }
        public IActionResult AddResult([FromForm]OnlineTurnVm req)
        {
            var filename = FileManager.IFormSaveLocal(req.File, "OnlineQuerieResults");
            FileName fileName = new FileName();
            fileName.FileNameData = filename;
            var client = new ServiceNode<OnlineTurnVm, object>(_fc).PostClient(fileName, $"/api/online_query/add/result/{req.query_id}");
            return RedirectToAction("OnlineTurnSection");
        }
        [HttpGet("/wp")]
        public IActionResult Test()
        {
            var client = new RestClient("https://api.releans.com/v2/message");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer 5edde0cac12d637e313b0967c2420d9e");
            request.AddParameter("sender", "IstanbulNs");
            request.AddParameter("mobile", "994506308522");
            request.AddParameter("content", "Hello");

            IRestResponse response = client.Execute(request);
            return Ok(new { code = response.StatusCode,body= response.Content});
        }
        public IActionResult GetDoctor([FromQuery] int id)
        {
            List<Doctor> doctor = new ServiceNode<object, List<Doctor>>(_fc).GetClient($"/api/online_query/doctorbyserviceid/{id}").Data;
            return Ok(doctor);
        }
        public IActionResult GetDoctorPrice([FromQuery] int id)
        {
            QueryDoctor doctor = new ServiceNode<object, QueryDoctor>(_fc).GetClient($"/api/online_query/doctorbyid/{id}").Data;
            return Ok(doctor);
        }



        /// <summary>
        /// Realqueries
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IActionResult> ConfirmReal([FromForm] OnlineTurnAddViewModel request)
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
            var resp = await new ServiceNodeAsync<OnlineQueryDTO, object>(_localizer, _fc).PostClientAsync(resources, "/api/online_query/confirm/real");
            if (resp.IsCatched == 1)
            {
                TempData["ServerResponseErrorAdmin"] = resp.Message.ToString();
                return RedirectToAction("NewQuery", "OnlineTurn");
            }
            #endregion
            TempData["ServerResponseSuccessAdmin"] = code;
            return RedirectToAction("OnlineTurnSectionReal", "OnlineTurn");
        }
        public async Task<IActionResult> OnlineTurnSectionReal([FromQuery] int page = 1)
        {
            ReturnMessage<List<OnlineQuery>> sv = await new ServiceNodeAsync<object, List<OnlineQuery>>(_fc).GetClientAsync($"/api/online_query/getall/real/{page}");
            OnlineTurnVm model = new OnlineTurnVm();
            model.queries = sv.Data;
            return View(model);
        }
        public IActionResult NewQuery()
        {
            var servis = new ServiceNode<object, OnlineTurnDTO>(_fc).GetClient("/api/online_query/info/3/1");
            var services = new ServiceNode<object, List<ServiceDTO>>(_fc).GetClient("/api/services/services/1");
            var model = new OnlineTurnAddViewModel();
            model.OnlineData = servis.Data;
            model.Services = services.Data;
            return View(model);
        }
        public IActionResult DisableR([FromQuery] int query_id)
        {
            var client = new ServiceNode<object, object>(_fc).GetClient($"/api/online_query/disable/{query_id}");
            return RedirectToAction("OnlineTurnSectionReal");
        }
        public IActionResult EnableR([FromQuery] int query_id)
        {
            var client = new ServiceNode<object, object>(_fc).GetClient($"/api/online_query/enable/{query_id}");
            return RedirectToAction("OnlineTurnSectionReal");
        }
        public IActionResult EndSessionR([FromQuery] int query_id)
        {
            var client = new ServiceNode<object, object>(_fc).GetClient($"/api/online_query/endsession/{query_id}");
            return RedirectToAction("OnlineTurnSectionReal");
        }
        public IActionResult AddResultR([FromForm] OnlineTurnVm req)
        {
            var filename = FileManager.IFormSaveLocal(req.File, "OnlineQuerieResults");
            FileName fileName = new FileName();
            fileName.FileNameData = filename;
            var client = new ServiceNode<OnlineTurnVm, object>(_fc).PostClient(fileName, $"/api/online_query/add/result/{req.query_id}");
            return RedirectToAction("OnlineTurnSectionReal");
        }
    }
}
