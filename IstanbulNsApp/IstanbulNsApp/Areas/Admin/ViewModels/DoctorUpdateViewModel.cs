using IstanbulNsApp.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Areas.Admin.ViewModels
{
    public class DoctorUpdateViewModel:DoctorAddViewModel
    {
        public DoctorAdmin DoctorsBase { get; set; }
    }
}
