using IstanbulNs.Data;
using IstanbulNs.Models;
using IstanbulNs.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IstanbulNs.Repositories.ReturnMessage;

namespace IstanbulNs.Controllers
{
    [Route("api/contactus")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IndexContext _db;
        public ContactUsController(IndexContext db)
        {
            _db = db;
        }
        [HttpGet, Route("")]

        public IActionResult GetContactParams()
        {
            var data = _db.ContactUs.ToList();
            if (data.Count == 0)
            {
                return NotFound(new ReturnMessage(data: data));
            }
            else
            {
                return Ok(new ReturnMessage(data: data));
            }
        }
        [HttpGet, Route("{id}")]

        public IActionResult GetContactParamsById(int id)
        {
            var data = _db.ContactUs.Find(id);
            if (data == null)
            {
                return NotFound(new ReturnMessage(data: data));
            }
            else
            {
                return Ok(new ReturnMessage(data: data));
            }
        }
        [HttpPost, Route("")]

        public IActionResult CreateContactParams([FromBody] ContactUs request)
        {
            var data = _db.ContactUs.Add(request);
            _db.SaveChanges();
            return Ok(new ReturnMessage());
        }
        [HttpPut, Route("{id}")]

        public IActionResult UpdateContactParams(int id, ContactUs request)
        {
            var data = _db.ContactUs.FirstOrDefault(h => h.Id == id);
            if (data == null)
            {
                return NotFound(new ReturnMessage(data: data));
            }
            else
            {
                data.Email = request.Email;
                data.Phone = request.Phone;
                data.Location = request.Location;
                data.WorkingWeek = request.WorkingWeek;
                data.WorkingTime = request.WorkingTime;
                _db.SaveChanges();
                return Ok(new ReturnMessage(data: data));
            }
        }
        [HttpDelete, Route("{id}")]

        public IActionResult DeleteContactParams(int id, ContactUs request)
        {
            var data = _db.ContactUs.Find(id);
            if (data == null)
            {
                return NotFound(new ReturnMessage(data: data));
            }
            else
            {
                _db.ContactUs.Remove(data);
                _db.SaveChanges();
                return Ok(new ReturnMessage(data: data));
            }
        }
    }
}
