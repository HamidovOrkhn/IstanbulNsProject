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
    [Route("api/doctors")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IndexContext _db;
        public DoctorController(IndexContext db)
        {
            _db = db;
        }
        [HttpGet("all/{page}")]
        public async Task<IActionResult> Doctors(int page)
        {
            List<Doctor> data = await _db.Doctors.ToListAsync();
            float pagecount = data.Count;
            int count = (int)Math.Ceiling(pagecount / 3);
            return Ok(new ReturnMessage(data: new { Data = (data).Skip(page * 3).Take(3).ToList(), Number = count }));
        }
        [HttpGet("all/admin/{page}")]
        public async Task<IActionResult> DoctorsAdmin(int page)
        {
            List<Doctor> data = await _db.Doctors.ToListAsync();
            float pagecount = data.Count;
            int count = (int)Math.Ceiling(pagecount / 4);
            return Ok(new ReturnMessage(data: new { Data = (data).Skip(page * 4).Take(4).ToList(), Number = count }));
        }
        [HttpGet("find/{first}")]
        public async Task<IActionResult> Doctors(string first)
        {
            IQueryable<Doctor> data = _db.Doctors.Where(c => c.Name.StartsWith(first));
            return Ok(new ReturnMessage(data: new { Data = await data.ToListAsync() }));
        }
        [HttpGet("info/{doctorId}/{langId}")]
        public IActionResult DoctorInfo(int doctorId, int langId)
        {
            #region FunctionBody
            var data = (from doctor in _db.Doctors.AsParallel()
                        join doctorprm in _db.DoctorsInfos.AsParallel() on doctor.Id equals doctorprm.DoctorId
                        join info in _db.InfoStrs.AsParallel() on doctor.Id equals info.DoctorId
                        where info.LangId == langId && doctor.Id == doctorId
                        select new
                        {
                            doctor.Id,
                            doctor.Sex,
                            doctor.LevelType,
                            doctor.Picture,
                            doctor.Name,
                            doctor.DoctorLevel,
                            info.Info,
                            doctorprm.Location,
                            doctorprm.Email,
                            Phones = (from phone in _db.PhoneDocs
                                      where phone.DoctorId == doctor.Id
                                      select new
                                      {
                                          phone.PhoneNumber
                                      }),
                            doctorprm.WorkTimeFromDate,
                            doctorprm.WorkTimeToDate,
                            doctorprm.WorkTimeFromTime,
                            doctorprm.WorkTimeToTime
                        });

            return Ok(new ReturnMessage(data: data.FirstOrDefault()));
            #endregion
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddDoctor([FromBody] DoctorDTO request)
        {
            #region FunctionBody
            Doctor doctor = new Doctor();
            List<InfoStr> doctorInfos_str = new List<InfoStr>();
            List<DoctorsInfo> doctorInfos = new List<DoctorsInfo>();
            List<PhoneDoc> doctorPhones = new List<PhoneDoc>();

            doctor.DoctorLevel = request.DoctorLevel;
            doctor.LevelType = request.LevelType;
            doctor.ServiceId = request.ServiceId;
            doctor.Sex = request.Sex;
            if (request.Picture !=null)
            {
                doctor.Picture = request.Picture;
            }
            doctor.Name = request.Name;
            if (request.DoctorsInfos.Phones is { })
            {
                foreach (var item in request.DoctorsInfos.Phones)
                {
                    doctorPhones.Add(new PhoneDoc
                    {
                        Doctor = doctor,
                        PhoneNumber = item.PhoneNumber
                    });
                }

            }
            if (request.DoctorsInfos is { })
            {
                doctorInfos.Add(new DoctorsInfo
                {
                    Email = request.DoctorsInfos.Email,
                    Location = request.DoctorsInfos.Location,
                    Phones = doctorPhones,
                    WorkTimeFromDate = request.DoctorsInfos.WorkTimeFromDate,
                    WorkTimeToDate = request.DoctorsInfos.WorkTimeToDate,
                    WorkTimeFromTime = request.DoctorsInfos.WorkTimeFromTime,
                    WorkTimeToTime = request.DoctorsInfos.WorkTimeToTime,
                });
            }
            if (request.InfoStrs is { })
            {
                foreach (var item in request.InfoStrs)
                {
                    doctorInfos_str.Add(new InfoStr
                    {
                        Info = item.Info,
                        LangId = item.LangId
                    });
                }

            }
            doctor.InfoStrs = doctorInfos_str;
            doctor.DoctorsInfos = doctorInfos;
            await _db.Doctors.AddAsync(doctor);
            await _db.SaveChangesAsync();
            return Ok(new ReturnMessage());
            #endregion
        }
        [HttpPost("update/{Id}")]
        public async Task<IActionResult> UpdateDoctor(int Id, [FromBody] DoctorDTO request)
        {
            #region FunctionBody
            Doctor doctor = await _db.Doctors
            .Where(a => a.Id == Id)
            .Include(a => a.DoctorsInfos)
            .ThenInclude(a => a.Phones)
            .Include(a => a.InfoStrs)
            .FirstOrDefaultAsync();

            List<InfoStr> doctorInfos_str = new List<InfoStr>();
            List<DoctorsInfo> doctorInfos = new List<DoctorsInfo>();
            List<PhoneDoc> doctorPhones = new List<PhoneDoc>();

            doctor.DoctorLevel = request.DoctorLevel;
            doctor.LevelType = request.LevelType;
            doctor.ServiceId = request.ServiceId;
            doctor.Sex = request.Sex;
            if (request.Picture!=string.Empty)
            {
                doctor.Picture = request.Picture;
            }
            doctor.Name = request.Name;
            if (request.DoctorsInfos.Phones is { })
            {
                foreach (var item in request.DoctorsInfos.Phones)
                {
                    doctorPhones.Add(new PhoneDoc
                    {
                        DoctorId = Id,
                        PhoneNumber = item.PhoneNumber
                    });
                }

            }
            if (request.DoctorsInfos is { })
            {
                doctorInfos.Add(new DoctorsInfo
                {
                    DoctorId = Id,
                    Email = request.DoctorsInfos.Email,
                    Location = request.DoctorsInfos.Location,
                    Phones = doctorPhones,
                    WorkTimeFromDate = request.DoctorsInfos.WorkTimeFromDate,
                    WorkTimeToDate = request.DoctorsInfos.WorkTimeToDate,
                    WorkTimeFromTime = request.DoctorsInfos.WorkTimeFromTime,
                    WorkTimeToTime = request.DoctorsInfos.WorkTimeToTime,
                });
            }
            if (request.InfoStrs is { })
            {
                foreach (var item in request.InfoStrs)
                {
                    doctorInfos_str.Add(new InfoStr
                    {
                        DoctorId = Id,
                        Info = item.Info,
                        LangId = item.LangId
                    });
                }

            }
            QueryieExtension.TryUpdateManyToMany(_db, doctor.InfoStrs, doctorInfos_str, a => a.DoctorId == Id);
            QueryieExtension.TryUpdateManyToMany(_db, doctor.DoctorsInfos, doctorInfos, a => a.DoctorId == Id);
            QueryieExtension.TryUpdateManyToMany(_db, doctor.DoctorsInfos.FirstOrDefault().Phones, doctorPhones, a => a.DoctorId == Id);
            await _db.SaveChangesAsync();
            return Ok(new ReturnMessage());
            #endregion
        }

        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> DeleteDoctor(int Id)
        {
            #region FunctionBody
            Doctor doctor = await _db.Doctors
            .Where(a => a.Id == Id)
            .Include(a => a.DoctorsInfos)
            .ThenInclude(a => a.Phones)
            .Include(a => a.InfoStrs)
            .FirstOrDefaultAsync();

            _db.DoctorsInfos.RemoveRange(doctor.DoctorsInfos);
            _db.PhoneDocs.RemoveRange(doctor.DoctorsInfos.FirstOrDefault().Phones);
            _db.InfoStrs.RemoveRange(doctor.InfoStrs);
            _db.Doctors.Remove(doctor);
            _db.SaveChanges();
            return Ok(new ReturnMessage());
            #endregion
        }
        [HttpGet("get/{doctorId}")]
        public IActionResult DoctorsInfo(int doctorId)
        {
            IQueryable<Doctor> doctor = _db.Doctors
                .Where(a=>a.Id == doctorId)
                .Include(a => a.DoctorsInfos)
                .ThenInclude(a => a.Phones)
                .Include(a => a.InfoStrs);
            return Ok(new ReturnMessage(doctor.FirstOrDefault()));
        }
      


    }
}
