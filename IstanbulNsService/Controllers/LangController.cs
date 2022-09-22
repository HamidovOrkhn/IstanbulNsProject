using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IstanbulNs.Data;
using IstanbulNs.Repositories;
using IstanbulNs.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IstanbulNs.Controllers
{
    [Route("api/lang")]
    [ApiController]
    public class LangController : ControllerBase
    {
        private readonly IndexContext _db;
        public LangController(IndexContext db)
        {
            _db = db;
        }
        [HttpGet("all")]
        public async Task<IActionResult> Get()
        {
            return Ok(new ReturnMessage(data:await _db.Langs.Select(a => new { Id = a.Id, Name = a.Name, Key = a.Key }).ToListAsync()));
        }
        [HttpGet("get/{key}")]
        public async Task<IActionResult> Get(string key)
        {
            return Ok(new ReturnMessage(data: await _db.Langs
                .Where(a =>a.Key == key)
                .Select(a => new { Id = a.Id, Name = a.Name, Key = a.Key })
                .FirstOrDefaultAsync()));
        }
        [HttpPost]
        public IActionResult Get([FromBody]AddServiceDTO addServiceDTO)
        {
            return Ok(addServiceDTO);
        }

    }
}
