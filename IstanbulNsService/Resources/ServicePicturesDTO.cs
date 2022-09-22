using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNs.Resources
{
    public class ServicePicturesDTO
    {
        public int Id { get; set; }
        public string Picture { get; set; }
        [Required]
        public int ServiceId { get; set; }
    }
}
