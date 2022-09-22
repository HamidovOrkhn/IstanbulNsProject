using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNs.Resources
{
    public class ServiceInfoDTO:BaseEntityDTO
    {
        public string Name { get; set; }
        [Required]
        public int ServiceId { get; set; }
    }
}
