using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Resources.DTO
{
    public class ServiceInfo
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Info { get; set; }
        public string? Picture { get; set; }
        public List<DoctorDTO>? Doctors { get; set; }
    }
}
