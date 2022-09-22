using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Areas.Admin.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage ="Email xanası doldurulmalıdır")]
        [MaxLength(200,ErrorMessage ="Email uzunluğu 200 şrifti keçə bilməz")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifrə xanası doldurulmalıdır")]
        [MaxLength(200, ErrorMessage = "Şifrə uzunluğu 200 şrifti keçə bilməz")]
        [MinLength(6,ErrorMessage = "Şifrə uzunluğu 6 şriftdən az ola bilməz")]
        public string Password { get; set; }
    }
}
