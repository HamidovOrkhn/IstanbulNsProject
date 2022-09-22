using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNs.Models
{
    public class ServiceInfo:BaseEntity
    {
        public string Name { get; set; }
        [Required]
        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
