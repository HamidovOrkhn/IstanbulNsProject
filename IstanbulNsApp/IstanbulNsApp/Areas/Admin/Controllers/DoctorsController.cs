using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IstanbulNsApp.Areas.Admin.Models;
using IstanbulNsApp.Areas.Admin.ViewModels;
using IstanbulNsApp.Filters;
using IstanbulNsApp.Libs;
using IstanbulNsApp.Models;
using IstanbulNsApp.Repositories;
using IstanbulNsApp.Resources.DTO;
using IstanbulNsApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using static IstanbulNsApp.Resources.Enums.RoleEnum;

namespace IstanbulNsApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AuthUser((int)Roles.SubAdmin)]
    public class DoctorsController : Controller
    {
        private readonly HttpClient _fc;
        private readonly IConfiguration _configuration;
        private readonly IStringLocalizer<SharedResource> _localizer;
        public DoctorsController(IHttpClientFactory fc, IConfiguration config, IStringLocalizer<SharedResource> localizer)
        {
            _fc = fc.CreateClient(name: "ApiRequests");
            _configuration = config;
            _localizer = localizer;
        }
        public IActionResult DoctorRaw([FromQuery] int page = 0)
        {
            int pagecount = page;
            var services = new ServiceNode<object, DoctorViewModel>(_fc).GetClient("/api/doctors/all/admin/" + pagecount);
            return View("DoctorModel", services.Data);
        }
        public IActionResult DoctorInfo([FromQuery] int doctorId)
        {
            DoctorAdmin doctors = new ServiceNode<object, DoctorAdmin>(_fc).GetClient($"/api/doctors/get/{doctorId}").Data;
            QueryCount count = new ServiceNode<object, QueryCount>(_fc).GetClient($"/api/online_query/query/count/{doctorId}").Data;
            List<Lang> langs = new ServiceNode<object, List<Lang>>(_fc).GetClient("/api/lang/all").Data;
            var model = new DoctorsViewModel();
            model.QCount = count.QCount;
            model.Doctor = doctors;
            model.Languages = langs;
            ViewData["DoctorId"] = doctorId;
            return View(model);
        }
        public IActionResult Edit([FromQuery] int doctorId)
        {
            #region LoadEditableData
            DoctorAdmin doctors = new ServiceNode<object, DoctorAdmin>(_fc).GetClient($"/api/doctors/get/{doctorId}").Data;
            List<LangDTO> langs = new ServiceNode<object, List<LangDTO>>(_fc).GetClient("/api/lang/all").Data;
            List<ServiceDTO> services = new ServiceNode<object, List<ServiceDTO>>(_fc).GetClient("/api/services/services/1").Data;
            var model = new DoctorUpdateViewModel();
            model.Langs = langs;
            model.Services = services;
            model.DoctorsBase = doctors;
            #endregion
            return View(model);
        }
        public IActionResult EditConfirm([FromForm] DoctorAddForm request)
        {
            #region EditDoctorInfo
            List<LangDTO> langs = new ServiceNode<object, List<LangDTO>>(_fc).GetClient("/api/lang/all").Data;
            DoctorAdd doctor = new DoctorAdd();
            doctor.DoctorLevel = request.DoctorLevel;
            doctor.Sex = request.Sex;
            doctor.ServiceId = request.ServiceId;
            doctor.Name = request.Name;
            doctor.LevelType = request.LevelType;
            List<InfoStr> infostrs = new List<InfoStr>();
            List<IstanbulNsApp.Areas.Admin.Models.Phone> phones = new List<IstanbulNsApp.Areas.Admin.Models.Phone>();
            for (int i = 0; i < langs.Count; i++)
            {
                infostrs.Add(new InfoStr
                {
                    LangId = langs[i].Id,
                    Info = request.InfoStrs[i]
                });
            }
            for (int i = 0; i < request.Phones.Count; i++)
            {
                phones.Add(new IstanbulNsApp.Areas.Admin.Models.Phone
                {
                    PhoneNumber = request.Phones[i]
                });
            }
            string pictureName = string.Empty;
            if (request.Picture != null)
            {
                if (request.ExistedPicture !=null)
                {
                    FileManager.Delete(request.ExistedPicture, "DoctorPictures");
                }
                pictureName = FileManager.IFormSaveLocal(request.Picture, "DoctorPictures");
            }
            doctor.InfoStrs = infostrs;
            doctor.Picture = pictureName;

            ///  DoctorInfos

            DoctorsInfo dcInfo = new DoctorsInfo();
            dcInfo.Location = request.Location;
            dcInfo.Phones = phones;
            dcInfo.WorkTimeFromDate = request.WorkTimeFromDate;
            dcInfo.WorkTimeToDate = request.WorkTimeToDate;
            dcInfo.WorkTimeToTime = request.WorkTimeToTime;
            dcInfo.WorkTimeFromTime = request.WorkTimeFromTime;
            dcInfo.Email = request.Email;
            doctor.DoctorsInfos = dcInfo;
            var req = new ServiceNode<DoctorAdd, object>(_localizer, _fc).PostClient(doctor, $"/api/doctors/update/{request.Id}");
            if (req.IsCatched == 1)
            {
                return RedirectToAction("Edit");
            }
            #endregion
            TempData["R_Message_Doc"] = "Dəyişdirildi";
            return RedirectToAction("Doctors", "Home");
        }
        public IActionResult Add()
        {
            #region LoadAddData
            List<LangDTO> langs = new ServiceNode<object, List<LangDTO>>(_fc).GetClient("/api/lang/all").Data;
            List<ServiceDTO> services = new ServiceNode<object, List<ServiceDTO>>(_fc).GetClient("/api/services/services/1").Data;
            var model = new DoctorAddViewModel();
            model.Langs = langs;
            model.Services = services;
            #endregion
            return View(model);
        }
        [HttpPost]
        public IActionResult AddConfirm([FromForm] DoctorAddForm request)
        {
            #region AddDoctor
            List<LangDTO> langs = new ServiceNode<object, List<LangDTO>>(_fc).GetClient("/api/lang/all").Data;
            DoctorAdd doctor = new DoctorAdd();
            doctor.DoctorLevel = request.DoctorLevel;
            doctor.Sex = request.Sex;
            doctor.ServiceId = request.ServiceId;
            doctor.Name = request.Name;
            doctor.LevelType = request.LevelType;
            List<InfoStr> infostrs = new List<InfoStr>();
            List<IstanbulNsApp.Areas.Admin.Models.Phone> phones = new List<IstanbulNsApp.Areas.Admin.Models.Phone>();
            for (int i = 0; i < langs.Count; i++)
            {
                infostrs.Add(new InfoStr
                {
                    LangId = langs[i].Id,
                    Info = request.InfoStrs[i]
                });
            }
            for (int i = 0; i < request.Phones.Count; i++)
            {
                phones.Add(new IstanbulNsApp.Areas.Admin.Models.Phone
                {
                    PhoneNumber = request.Phones[i]
                });
            }
            string pictureName = string.Empty;
            if (request.Picture != null)
            {
                pictureName = FileManager.IFormSaveLocal(request.Picture, "DoctorPictures");
            }
            doctor.InfoStrs = infostrs;
            doctor.Picture = pictureName;

            ///  DoctorInfos
            
            DoctorsInfo dcInfo = new DoctorsInfo();
            dcInfo.Location = request.Location;
            dcInfo.Phones = phones;
            dcInfo.WorkTimeFromDate = request.WorkTimeFromDate;
            dcInfo.WorkTimeToDate = request.WorkTimeToDate;
            dcInfo.WorkTimeToTime = request.WorkTimeToTime;
            dcInfo.WorkTimeFromTime = request.WorkTimeFromTime;
            dcInfo.Email = request.Email;
            doctor.DoctorsInfos = dcInfo;
            var req = new ServiceNode<DoctorAdd, object>(_localizer, _fc).PostClient(doctor, "/api/doctors/add");
            if (req.IsCatched == 1)
            {
                return RedirectToAction("Add");
            }
            #endregion
            TempData["R_Message_Doc"] = "Əlavə olundu";
            return RedirectToAction("Doctors", "Home");
        }
        public IActionResult Delete([FromQuery] int doctorId)
        {            
            var req = new ServiceNode<object, object>(_localizer, _fc).DeleteClient($"/api/doctors/delete/{doctorId}");
            TempData["R_Message_Doc"] = "Silindi";
            return RedirectToAction("Doctors", "Home", new { Area = "Admin" });
        }
    }
}
