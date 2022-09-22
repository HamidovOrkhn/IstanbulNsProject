using IstanbulNs.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNs.Resources
{
    public class ServiceNameDTO:BaseEntityDTO
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        public int ServiceId { get; set; }
  
    }
}
