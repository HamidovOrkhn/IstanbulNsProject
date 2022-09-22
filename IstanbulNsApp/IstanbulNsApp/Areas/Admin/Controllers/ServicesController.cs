using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IstanbulNsApp.Areas.Admin.Models;
using IstanbulNsApp.Areas.Admin.ViewModels;
using IstanbulNsApp.Areas.ViewModels;
using IstanbulNsApp.Filters;
using IstanbulNsApp.Libs;
using IstanbulNsApp.Repositories;
using IstanbulNsApp.Resources.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using static IstanbulNsApp.Resources.Enums.RoleEnum;

namespace IstanbulNsApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthUser((int)Roles.SubAdmin)]
    public class ServicesController : Controller
    {
        private readonly HttpClient _fc;
        private readonly IConfiguration _configuration;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public ServicesController(IHttpClientFactory fc, IConfiguration config, IStringLocalizer<SharedResource> localizer)
        {
            _fc = fc.CreateClient(name: "ApiRequests");
            _configuration = config;
            _localizer = localizer;
        }
        public async Task<IActionResult> Add()
        {
            #region AddData 
            var client = new ServiceNodeAsync<object, List<LangDTO>>(_fc);
            var services = await client.GetClientAsync("/api/lang/all");
            AddServiceViewModel model = new AddServiceViewModel();
            model.Langs = services.Data;
            #endregion
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddConfirm([FromForm] AddServiceViewModel data)
        {
            #region AddService
            var client = new ServiceNodeAsync<object, List<LangDTO>>(_fc);
            var service = await client.GetClientAsync("/api/lang/all");
            List<LangDTO> langs = service.Data;
            List<ServiceNameDTO> names = new List<ServiceNameDTO>();
            List<ServiceInfoDTO> infos = new List<ServiceInfoDTO>();
            AddServiceDTO model = new AddServiceDTO();
            for (int i = 0; i < data.NumberLang; i++)
            {
                names.Add(new ServiceNameDTO
                {
                    LangId = langs[i].Id,
                    Name = data.Names[i],
                    ServiceId = 12

                });
                infos.Add(new ServiceInfoDTO
                {
                    LangId = langs[i].Id,
                    Name = data.Infos[i],
                    ServiceId = 12

                });               
            }
            string pictureName = string.Empty;
            if (data.Picture != null)
            {
                pictureName = FileManager.IFormSaveLocal(data.Picture, "ServicePictures");
            }
            
            model.IsPayed = data.IsPayed;
            model.ServicePictures = new ServicePictureDTO {Picture = pictureName, ServiceId= 12 };
            model.ServiceNames = names;
            model.ServiceInfo = infos;
            var req = await new ServiceNodeAsync<AddServiceDTO, object>(_localizer, _fc).PostClientAsync(model, "/api/services/add/new");
            if (req.IsCatched == 1)
            {
                return RedirectToAction("Add");
            }
            #endregion
            TempData["R_Message"] = "Əlavə olundu";
            return RedirectToAction("Services","Home",new { Area = "Admin"});
        }
        public IActionResult Delete([FromQuery] int query_id)
        {
            var req =  new ServiceNode<object, object>(_localizer, _fc).DeleteClient($"/api/services/delete/{query_id}");
            TempData["R_Message"] = "Silindi";
            return RedirectToAction("Services", "Home", new { Area = "Admin" });
        }
        public IActionResult Update([FromQuery] int query_id)
        {
            #region GetUpdatedData
            var client = new ServiceNode<object, List<LangDTO>>(_fc);
            var service =  client.GetClient("/api/lang/all");
            var req = new ServiceNode<object, ServiceDTOAdmin>(_fc).GetClient($"/api/services/get/{query_id}");
            List<LangDTO> langs = service.Data;
            UpdateServiceViewModel model = new UpdateServiceViewModel();
            model.Langs = langs;
            model.Services = req.Data;           
            #endregion
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateConfirm([FromForm] UpdateServiceViewModel data)
        {
            #region UpdateService
            var client = new ServiceNodeAsync<object, List<LangDTO>>(_fc);
            var service = await client.GetClientAsync("/api/lang/all");
            List<LangDTO> langs = service.Data;
            List<ServiceNameDTO> names = new List<ServiceNameDTO>();
            List<ServiceInfoDTO> infos = new List<ServiceInfoDTO>();
            AddServiceDTO model = new AddServiceDTO();
            for (int i = 0; i < data.NumberLang; i++)
            {
                names.Add(new ServiceNameDTO
                {
                    LangId = langs[i].Id,
                    Name = data.Names[i],
                    ServiceId = 12

                });
                infos.Add(new ServiceInfoDTO
                {
                    LangId = langs[i].Id,
                    Name = data.Infos[i],
                    ServiceId = 12

                });
            }
            string pictureName = string.Empty;
            if (data.Picture != null)
            {
                pictureName = FileManager.IFormSaveLocal(data.Picture, "ServicePictures");
            }

            model.IsPayed = data.IsPayed;
            model.ServicePictures = new ServicePictureDTO { Picture = pictureName, ServiceId = 12 };
            model.ServiceNames = names;
            model.ServiceInfo = infos;
            var req = await new ServiceNodeAsync<AddServiceDTO, object>(_localizer, _fc).PostClientAsync(model, "/api/services/update/"+data.Id);
            if (req.IsCatched == 1)
            {
                return RedirectToAction("Update",new { query_id = data.Id });
            }
            #endregion
            TempData["R_Message"] = "Dəyişdirildi";
            return RedirectToAction("Services", "Home", new { Area = "Admin" });
        }
    }
}
