using IstanbulNsApp.Resources.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.ViewModels
{
    public class DoctorViewModel
    {
        public List<DoctorDTO> Data { get; set; }
        public int? Number { get; set; }
    }
}
