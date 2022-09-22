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
    [Route("api/blog")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IndexContext _db;
        public BlogController(IndexContext db)
        {
            _db = db;
        }
        [HttpGet("{langId}")]
        public IActionResult GetBlogData(int langId)
        {
            var data = from blog in _db.Blogs.ToList()
                       where blog.LangId == langId
                       select new
                       {
                           blog.Id,
                           blog.Photo,
                           blog.Text,
                           blog.Date,
                           blog.Title
                       };
            return Ok(new ReturnMessage(data: data));
        }
        [HttpGet("single/{id}")]
        public IActionResult GetBlogfDataById(int id)
        {
            Blog data = _db.Blogs.Find(id);
            List<Blog> datas = _db.Blogs.ToList();
            BlogViewModel model = new BlogViewModel { Blog = data, Blogs = datas };
            return Ok(new ReturnMessage(data: model));
        }
    }
}
