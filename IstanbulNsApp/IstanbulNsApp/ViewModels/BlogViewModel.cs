using IstanbulNsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.ViewModels
{
    public class BlogViewModel
    {
        public List<Blog> Blogs { get; set; }
        public Blog Blog { get; set; }
    }
}
