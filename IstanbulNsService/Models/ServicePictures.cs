using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNs.Models
{
    public class ServicePictures
    {
        public int Id { get; set; }
        [Required]
        public string Picture { get; set; }
        [Required]
        public int ServiceId { get; set; }
        public Service Services { get; set; }
    }
}
