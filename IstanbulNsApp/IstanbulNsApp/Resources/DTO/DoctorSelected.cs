using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Resources.DTO
{
    public class DoctorSelected:DoctorBase
    {
        public DoctorWorkTimes DoctorInfo { get; set; }
        public int Price { get; set; }
    }
}
