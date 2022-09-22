using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNs.Resources
{
    public class ChangePassword
    {
        [Required]
        [MaxLength(200)]
        [MinLength(6)]
        public string OldPassword { get; set; }
        [Required]
        [MaxLength(200)]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
