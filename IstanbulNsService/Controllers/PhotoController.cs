using IstanbulNs.Data;
using IstanbulNs.Models;
using IstanbulNs.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNs.Controllers
{
    [Route("api/photo")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IndexContext _db;
        public PhotoController(IndexContext db)
        {
            _db = db;
        }
        [HttpGet, Route("list")]

        public IActionResult GetPhotoParams()
        {
            var data = _db.Photos.ToList();
            if (data.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(new ReturnMessage {Data= data});
            }
        }
        [HttpGet, Route("{id}")]

        public IActionResult GetPhotoParamsById(int id)
        {
            var data = _db.Photos.Find(id);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }
        [HttpPost, Route("")]

        public IActionResult CreatePhotoParams([FromBody] Photo request)
        {
            var data = _db.Photos.Add(request);
            _db.SaveChanges();
            return Ok("Created");
        }
        [HttpPut, Route("{id}")]

        public IActionResult UpdatePhotoParams(int id, Photo request)
        {
            var data = _db.Photos.FirstOrDefault(h => h.Id == id);
            if (data == null)
            {
                return NotFound("Info not Founded");
            }
            else
            {
                data.Name = request.Name;
                _db.SaveChanges();
                return Ok("Edited");
            }
        }
        [HttpDelete, Route("{id}")]

        public IActionResult DeletePhotoParams(int id, Photo request)
        {
            var data = _db.Photos.Find(id);
            if (data == null)
            {
                return NotFound("Info not Founded");
            }
            else
            {
                _db.Photos.Remove(data);
                _db.SaveChanges();
                return Ok("Deleted");
            }
        }
    }
}


