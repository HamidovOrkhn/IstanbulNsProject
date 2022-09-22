using IstanbulNsApp.Areas.Admin.Models;
using IstanbulNsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Areas.Admin.ViewModels
{
    public class DoctorsViewModel
    {
        public int QCount { get; set; }
        public DoctorAdmin Doctor { get; set; }
        public List<Lang> Languages { get; set; }
    }
}
