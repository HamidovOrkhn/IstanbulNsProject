using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Resources.DTO
{
    public class DoctorDTO:DoctorBase
    {
        public string? DoctorLevel { get; set; }
        public string? Picture { get; set; }
        public int? Sex { get; set; }
        public int? LevelType { get; set; }
    }
}
