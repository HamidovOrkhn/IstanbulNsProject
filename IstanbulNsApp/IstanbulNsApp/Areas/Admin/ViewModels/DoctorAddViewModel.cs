using IstanbulNsApp.Areas.Admin.Models;
using IstanbulNsApp.Models;
using IstanbulNsApp.Resources.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Areas.Admin.ViewModels
{
    public class DoctorAddViewModel:DoctorAddForm
    {
        public List<LangDTO> Langs { get; set; }
        public List<ServiceDTO> Services { get; set; }
    }
}
