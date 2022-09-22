using IstanbulNsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.ViewModels
{
    public class HomeViewModel
    {
        public Home Home { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Photo> Photos { get; set; }
    }
}
