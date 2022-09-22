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
    [Route("api/video")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IndexContext _db;
        public VideoController(IndexContext db)
        {
            _db = db;
        }
        [HttpGet, Route("list")]

        public IActionResult GetVideoParams()
        {
            var data = _db.Videos.ToList();
            if (data.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(new ReturnMessage { Data = data });
            }
        }
        [HttpGet, Route("{id}")]

        public IActionResult GetVideoParamsById(int id)
        {
            var data = _db.Videos.Find(id);
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

        public IActionResult CreateVideoParams([FromBody] Video request)
        {
            var data = _db.Videos.Add(request);
            _db.SaveChanges();
            return Ok("Created");
        }
        [HttpPut, Route("{id}")]

        public IActionResult UpdateVideoParams(int id, Video request)
        {
            var data = _db.Videos.FirstOrDefault(h => h.Id == id);
            if (data == null)
            {
                return NotFound("Info not Founded");
            }
            else
            {
                data.Source = request.Source;
                _db.SaveChanges();
                return Ok("Edited");
            }
        }
        [HttpDelete, Route("{id}")]

        public IActionResult DeleteVideoParams(int id, Video request)
        {
            var data = _db.Videos.Find(id);
            if (data == null)
            {
                return NotFound("Info not Founded");
            }
            else
            {
                _db.Videos.Remove(data);
                _db.SaveChanges();
                return Ok("Deleted");
            }
        }
    }
}
