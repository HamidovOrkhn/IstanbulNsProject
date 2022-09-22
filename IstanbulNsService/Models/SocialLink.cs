using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNs.Models
{
    public class SocialLink
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Icon { get; set; }
        [Required, MaxLength(200)]
        public string Url { get; set; }
        [Required]
        public int OrderBy { get; set; }
    }
}
