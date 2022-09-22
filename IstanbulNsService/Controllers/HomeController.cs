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
    [Route("api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IndexContext _db;
        public HomeController(IndexContext db)
        {
            _db = db;
        }
        [HttpGet, Route("{langId}")]
        
        public  IActionResult GetHomeParams(int langId)
        {
            var data = _db.Home.Where(h=>h.LangId == langId).FirstOrDefault();
            if (data == null)
            {
                return NotFound(new ReturnMessage(data: data));
            }
            else
            {
                return Ok(new ReturnMessage(data: data));
            }
        }
    }
}
