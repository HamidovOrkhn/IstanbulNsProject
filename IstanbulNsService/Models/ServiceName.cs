using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNs.Models
{
    public class ServiceName:BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        public int ServiceId { get; set; }
        public Service Services { get; set; }
    }
}
