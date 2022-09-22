using IstanbulNsApp.Resources.DTO;
using IstanbulNsApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Areas.Admin.ViewModels
{
    public class OnlineTurnAddViewModel:OnlineTurnViewModel
    {
        public List<ServiceDTO> Services { get; set; }
    }
}
