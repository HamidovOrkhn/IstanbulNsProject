using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IstanbulNs.Data;
using IstanbulNs.Models;
using IstanbulNs.Repositories;
using IstanbulNs.Repositories.Enums;
using IstanbulNs.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace IstanbulNs.Controllers
{
    
    [Route("api/online_query")]
    [ApiController]
    public class OnlineQueryController : ControllerBase
    {
        private readonly IndexContext _db;
        public OnlineQueryController(IndexContext db)
        {
            _db = db;
        }
        [HttpGet("info/{doctor}/{langId}")]
        public async Task<IActionResult> GetExternedInfo(int doctor, int langId)
        {
            #region FunctionBody
            var doctorSelected = await _db.Doctors.Where(a => a.Id == doctor).Include("DoctorsInfos").Select(
                a => new
                {
                    Id = a.Id,
                    Name = a.Name,
                    ServiceId = a.ServiceId,
                    DoctorInfo = a.DoctorsInfos.FirstOrDefault(),
                    Price = a.Price
                }).FirstOrDefaultAsync();

            var service = (from services in _db.Services.AsParallel()
                           join name in _db.ServiceNames.AsParallel() on services.Id equals name.ServiceId
                           where services.Id == doctorSelected.ServiceId && name.LangId == langId
                           select new
                           {
                               services.Id,
                               name.Name
                           });
            return Ok(new ReturnMessage(data: new { doctorSelected = doctorSelected, service = service.FirstOrDefault() }));
            #endregion
        }
        [HttpPost("confirm")]
        public async Task<IActionResult> Confirm([FromBody] OnlineQuery request)
        {
            #region FunctionBody
            bool existed = await _db.OnlineQueries.AnyAsync(
                a =>
                a.QueryDate == request.QueryDate &&
                a.IsDeleted == 0 &&
                a.IsSchedule == 1);
            if (existed)
            {
                return BadRequest(new ReturnErrorMessage(errortype: (int)ErrorTypes.Errors.ExistedTime, message: "This Time is exist by other user"));
            }
            Doctor dcBase = await _db.Doctors.FirstOrDefaultAsync(a=>a.Id == request.DoctorId);
            DoctorsInfo doctor = await _db.DoctorsInfos.FirstOrDefaultAsync(a => a.DoctorId == request.DoctorId);
            if (
                doctor.WorkTimeFromDate > (int)request.QueryDate.DayOfWeek ||
                doctor.WorkTimeToDate < (int)request.QueryDate.DayOfWeek
               )

            {
                return BadRequest(new ReturnErrorMessage(errortype: (int)ErrorTypes.Errors.NotAllowedTime, message: "This Time not registered by Doctor"));
            }
            try
            {
                request.ServeDate = DateTime.Now;
                request.IsSchedule = 1;
                request.IsOnline = 1;
                request.Price = dcBase.Price;
                await _db.OnlineQueries.AddAsync(request);

            }
            catch (Exception x)
            {
                return BadRequest(new ReturnErrorMessage(errortype: (int)ErrorTypes.Errors.Internal, message: x.Message, code: 500));
            }
            await _db.SaveChangesAsync();
            return Ok(new ReturnMessage());
            #endregion
        }
        [HttpPost("confirm/real")]
        public async Task<IActionResult> ConfirmReal([FromBody] OnlineQuery request)
        {
            #region FunctionBody
            bool existed = await _db.OnlineQueries.AnyAsync(
                a =>
                a.QueryDate == request.QueryDate &&
                a.IsDeleted == 0 &&
                a.IsSchedule == 1);
            if (existed)
            {
                return BadRequest(new ReturnErrorMessage(errortype: (int)ErrorTypes.Errors.ExistedTime, message: "This Time is exist by other user"));
            }
            Doctor dcBase = await _db.Doctors.FirstOrDefaultAsync(a => a.Id == request.DoctorId);
            DoctorsInfo doctor = await _db.DoctorsInfos.FirstOrDefaultAsync(a => a.DoctorId == request.DoctorId);
            if (
                doctor.WorkTimeFromDate > (int)request.QueryDate.DayOfWeek ||
                doctor.WorkTimeToDate < (int)request.QueryDate.DayOfWeek
               )

            {
                return BadRequest(new ReturnErrorMessage(errortype: (int)ErrorTypes.Errors.NotAllowedTime, message: "This Time not registered by Doctor"));
            }
            try
            {
                request.ServeDate = DateTime.Now;
                request.IsSchedule = 1;
                request.IsOnline = 0;
                request.Price = dcBase.Price;
                await _db.OnlineQueries.AddAsync(request);

            }
            catch (Exception x)
            {
                return BadRequest(new ReturnErrorMessage(errortype: (int)ErrorTypes.Errors.Internal, message: x.Message, code: 500));
            }
            await _db.SaveChangesAsync();
            return Ok(new ReturnMessage());
            #endregion
        }
        [HttpGet("getall/{pageRequested}")]
        public IActionResult GetAllQueriesOnByPagination(int pageRequested)
        {
            IQueryable<OnlineQuery> queries = _db.OnlineQueries.Where(a => a.IsDeleted == 0 && a.IsOnline == 1)
                .Include(a => a.Pdf)
                .Include(a => a.Doctor)
                .ThenInclude(a => a.Service)
                .ThenInclude(a=>a.ServiceNames);


            return Ok(new ReturnMessage(data: queries));
        }
        [HttpGet("getall/real/{pageRequested}")]
        public IActionResult GetAllQueriesOnByPaginationReal(int pageRequested)
        {
            IQueryable<OnlineQuery> queries = _db.OnlineQueries.Where(a => a.IsDeleted == 0 && a.IsOnline == 0)
                .Include(a => a.Pdf)
                .Include(a => a.Doctor)
                .ThenInclude(a => a.Service)
                .ThenInclude(a => a.ServiceNames);


            return Ok(new ReturnMessage(data: queries));
        }

        [HttpPost("add/result/{query_id}")]
        public async Task<IActionResult> AddResult(int query_id, [FromBody]FileName fl)
        {
            try
            {
                await _db.PDFBase.AddAsync(new Pdf
                {
                    FileName = fl.FileNameData,
                    OnlineQueryId = query_id
                });
                await _db.SaveChangesAsync();
            }
            catch (Exception x)
            {
                return StatusCode(500, new ReturnMessage(message: x.Message));
            }

            return Ok(new ReturnMessage());
        }
        [HttpGet("disable/{query_id}")]
        public async Task<IActionResult> Disable(int query_id)
        {
            OnlineQuery query = await _db.OnlineQueries.Where(a => a.Id == query_id).FirstOrDefaultAsync();
            query.IsSchedule = 0;
            await _db.SaveChangesAsync();
            return Ok(new ReturnMessage());
        }
        [HttpGet("enable/{query_id}")]
        public async Task<IActionResult> Enable(int query_id)
        {
            OnlineQuery query = await _db.OnlineQueries.Where(a => a.Id == query_id).FirstOrDefaultAsync();
            query.IsSchedule = 1;
            await _db.SaveChangesAsync();
            return Ok(new ReturnMessage());
        }
        [HttpGet("endsession/{query_id}")]
        public async Task<IActionResult> EndSession(int query_id)
        {
            OnlineQuery query = await _db.OnlineQueries.Where(a => a.Id == query_id).FirstOrDefaultAsync();
            query.IsDeleted = 1;
            await _db.SaveChangesAsync();
            return Ok(new ReturnMessage());
        }
        [HttpGet("query/count/{doctorId}")]
        public IActionResult QueryCount(int doctorId)
        {
            return Ok(new ReturnMessage(data: new QueryCount { QCount = _db.OnlineQueries.Where(a => a.DoctorId == doctorId).Count() }));
        }
        [HttpGet("doctorbyserviceid/{id}")]
        public async Task<IActionResult> DoctorByServiceId(int id)
        {
            var querie = await _db.Doctors.Where(a => a.ServiceId == id).ToListAsync();
            return Ok(new ReturnMessage(data:querie));
        }
        [HttpGet("doctorbyid/{id}")]
        public async Task<IActionResult> DoctorById(int id)
        {
            var querie = await _db.Doctors.Where(a => a.Id == id).FirstOrDefaultAsync();
            return Ok(new ReturnMessage(data: querie));
        }
        [HttpPost("result")]
        public IActionResult GetResult([FromBody] ClientCredentials request)
        {
            bool existqueryId = _db.OnlineQueries.Any(a=>a.Code == request.QueryId);
            if (!existqueryId)
            {
                return BadRequest(new ReturnErrorMessage(errortype:(int)ErrorTypes.Errors.CodeInvalid));
            }
            bool existNumber = _db.OnlineQueries.Any(a => a.PhoneNumber == request.PhoneNumber);
            if (!existNumber)
            {
                return BadRequest(new ReturnErrorMessage(errortype:(int)ErrorTypes.Errors.NumberInvalid));
            }
            OnlineQuery onlineQuery = _db.OnlineQueries.Where(a => a.Code == request.QueryId && a.PhoneNumber == request.PhoneNumber).FirstOrDefault();
            return Ok(new ReturnMessage(data:_db.PDFBase.Where(a=>a.OnlineQueryId == onlineQuery.Id).FirstOrDefault()));

        }

    }
}
