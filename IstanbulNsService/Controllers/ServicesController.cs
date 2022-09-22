using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IstanbulNs.Data;
using IstanbulNs.Lib;
using IstanbulNs.Models;
using IstanbulNs.Repositories;
using IstanbulNs.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IstanbulNs.Controllers
{
    [Route("api/services")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IndexContext _db;
        public ServicesController(IndexContext db)
        {
            _db = db;
        }
        [HttpGet("services/{langId}")]
        public IActionResult Services(int langId)
        {
            ParallelQuery queryable = (from service in _db.Services.AsParallel()
                                       join serviceInfo in _db.ServiceNames.AsParallel() on service.Id equals serviceInfo.ServiceId
                                       where serviceInfo.LangId == langId
                                       select new
                                       {
                                           service.Id,
                                           serviceInfo.Name
                                       }).OrderBy(a=>a.Id);
            return Ok(new ReturnMessage(data: queryable));

        }
        [HttpGet("info/{langId}/{serviceId}")]
        public IActionResult ServiceInfo(int langId, int serviceId)
        {
            var ServiceNames = _db.ServiceNames.Where(a => a.LangId == langId).ToList();
            var ServiceInfos = _db.ServiceInfo.Where(a => a.LangId == langId).ToList();
            var Services = _db.Services.Where(a => a.Id == serviceId).ToList();
            var queryable = from service in Services
                             join servicename in ServiceNames on service.Id equals servicename.ServiceId
                             join serviceinfo in ServiceInfos  on service.Id equals serviceinfo.ServiceId
                             //where service.Id == serviceId && servicename.LangId == langId && serviceinfo.LangId == langId
                             select new
                             {
                                 service.Id,
                                 servicename.Name,
                                 Info = serviceinfo.Name,
                                 Picture = _db.ServicePictures.Where(a=> a.ServiceId == serviceId).FirstOrDefault()?.Picture,
                                 Doctors = from dctr in _db.Doctors
                                            where dctr.ServiceId == serviceId
                                            select new
                                            {
                                                dctr.Id,
                                                dctr.Name,
                                                dctr.DoctorLevel,
                                                dctr.Picture,
                                                dctr.Sex,
                                                dctr.LevelType,
                                                dctr.Price
                                            }
                             };



            return Ok(new ReturnMessage(data: queryable.FirstOrDefault()));
        }
        [HttpGet("all/{langId}")]
        public IActionResult ServicesAll(int langId)
        {
            var data = from service in _db.Services
                       join names in _db.ServiceNames on service.Id equals names.ServiceId
                       join servicePictures in _db.ServicePictures on service.Id equals servicePictures.ServiceId into pctr
                       from pic in pctr.DefaultIfEmpty()
                       where names.LangId == langId
                       select new
                       {
                           service.Id,
                           names.Name,
                           pic.Picture
                       };
            return Ok(new ReturnMessage(data: data));
        }
        [HttpGet("get/{serviceId}")]
        public IActionResult ServicesGetOne(int serviceId)
        {
            #region FunctionBody
            var queryable = _db.Services.Where(a=>a.Id == serviceId)
                .Include(a => a.ServiceNames)
                .Include(a => a.ServicePictures)
                .Include(a => a.ServiceInfo); 
            return Ok(new ReturnMessage(data: queryable.FirstOrDefault()));
            #endregion
        }
        [HttpPost("add/new")]
        public async Task<IActionResult> AddService([FromBody] ServiceDTO request)
        {
            #region FunctionBody
            Service service = new Service();
            List<ServiceName> serviceNames = new List<ServiceName>();
            List<ServicePictures> servicePictures = new List<ServicePictures>();
            List<ServiceInfo> serviceInfos = new List<ServiceInfo>();
            service.IsPayed = request.IsPayed;
            if (request.ServiceNames is { })
            {
                foreach (var item in request.ServiceNames)
                {
                    serviceNames.Add(new ServiceName
                    {
                        LangId = item.LangId,
                        Name = item.Name,
                    });
                }
            }
            if (request.ServiceInfo is { })
            {
                foreach (var item in request.ServiceInfo)
                {
                    serviceInfos.Add(new ServiceInfo
                    {
                        LangId = item.LangId,
                        Name = item.Name
                    });
                }
            }
            if (request.ServicePictures.Picture != string.Empty)
            {
                servicePictures.Add(new ServicePictures
                {
                    Picture = request.ServicePictures.Picture
                });
            }

            service.ServiceNames = serviceNames;
            service.ServicePictures = servicePictures;
            service.ServiceInfo = serviceInfos;
            await _db.Services.AddAsync(service);
            await _db.SaveChangesAsync();
            return Ok(new ReturnMessage());
            #endregion
        }
        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> DeleteService(int Id)
        {
            #region FunctionBody
            Service serviceDeleted = await _db.Services
                .Include(a => a.ServiceInfo)
                .Include(a => a.ServiceNames)
                .Include(a => a.ServicePictures)
                .FirstOrDefaultAsync(a => a.Id == Id);
            _db.Services.Remove(serviceDeleted);
            _db.ServiceNames.RemoveRange(serviceDeleted.ServiceNames);
            _db.ServiceInfo.RemoveRange(serviceDeleted.ServiceInfo);
            if (serviceDeleted.ServicePictures.Count > 0)
            {
                _db.ServicePictures.Remove(serviceDeleted.ServicePictures.FirstOrDefault());
            }
            await _db.SaveChangesAsync();
            return Ok(new ReturnMessage());
            #endregion
        }
        [HttpPost("update/{Id}")]
        public async Task<IActionResult> UpdateService(int Id, [FromBody] ServiceDTO request)
        {
            #region FunctionBody
            Service service = await _db.Services
                .Include(a => a.ServiceInfo)
                .Include(a => a.ServiceNames)
                .Include(a => a.ServicePictures)
                .FirstOrDefaultAsync(a => a.Id == Id);
            List<ServiceName> serviceNames = new List<ServiceName>();
            List<ServiceInfo> serviceInfos = new List<ServiceInfo>();
            service.IsPayed = request.IsPayed;
            if (request.ServiceNames is { })
            {
                foreach (var item in request.ServiceNames)
                {
                    serviceNames.Add(new ServiceName
                    {
                        LangId = item.LangId,
                        Name = item.Name,
                        ServiceId = Id
                    });
                }
            }
            if (request.ServiceInfo is { })
            {
                foreach (var item in request.ServiceInfo)
                {
                    serviceInfos.Add(new ServiceInfo
                    {
                        LangId = item.LangId,
                        Name = item.Name,
                        ServiceId = Id
                    });
                }
            }

            QueryieExtension.TryUpdateManyToMany(_db, service.ServiceNames, serviceNames, a => a.ServiceId == Id);
            QueryieExtension.TryUpdateManyToMany(_db, service.ServiceInfo, serviceInfos, a => a.ServiceId == Id);
            if (request.ServicePictures.Picture !=string.Empty)
            {
                ServicePictures servicePicture = await _db.ServicePictures.Where(a => a.ServiceId == Id).FirstOrDefaultAsync();
                if (servicePicture is null)
                {
                    _db.ServicePictures.Add(new ServicePictures { Picture = request.ServicePictures.Picture, ServiceId = Id });
                }
                else
                {
                    servicePicture.Picture = request.ServicePictures.Picture;
                }

            }           
            await _db.SaveChangesAsync();
            return Ok(new ReturnMessage());
            #endregion
        }
       








    }
}
