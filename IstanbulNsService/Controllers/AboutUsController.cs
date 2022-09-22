using IstanbulNs.Data;
using IstanbulNs.Models;
using IstanbulNs.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNs.Controllers
{
    [Route("api/aboutus")]
    [ApiController]
    public class AboutUsController : ControllerBase
    {
        private readonly IndexContext _db;
        public AboutUsController(IndexContext db)
        {
            _db = db;
        }
        [HttpGet("{langId}")]
        public IActionResult GetControllerData(int langId)
        {
            var data = _db.AboutUs.Include("Photos").Include("Videos").Where(i => i.LangId == langId).FirstOrDefault();
            return Ok(new ReturnMessage(data: data));
        }
        [HttpPost("create")]
        public IActionResult CreateControllerData([FromBody] AboutUs request)
        {

            return Ok();
        }
    }
}
