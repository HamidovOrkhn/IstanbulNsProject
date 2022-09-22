using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNs.Models
{
    public class Service
    {

        public int Id { get; set; }
        [Required]      
        [Range(1,10)]
        public int IsPayed { get; set; }
        public ICollection<ServiceName> ServiceNames { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<ServiceInfo> ServiceInfo { get; set; }
        // public ICollection<OnlineQuery> OnlineQuerys { get; set; }
        public ICollection<ServicePictures> ServicePictures { get; set; }

    }
}
