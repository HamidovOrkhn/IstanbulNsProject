
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Areas.Admin.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Sahə doldurulmalıdır")]
        [MaxLength(200, ErrorMessage = "Maximum Şrift sayını keçdiniz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Sahə doldurulmalıdır")]
        [MaxLength(200, ErrorMessage = "Maximum Şrift sayını keçdiniz")]
        [EmailAddress(ErrorMessage ="Email formatı düzgün deyil")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Sahə doldurulmalıdır")]
        public int SexId { get; set; }
        [Required(ErrorMessage = "Sahə doldurulmalıdır")]
        [MaxLength(200,ErrorMessage ="Maximum Şrift sayını keçdiniz")]
        [MinLength(6,ErrorMessage ="Minimum 6 Şrift olmalıdır")]
        public string Password { get; set; }
        [MaxLength(200)]
        public string Token { get; set; }
        [Required(ErrorMessage = "Sahə doldurulmalıdır")]
        public int Role { get; set; }
        public int IsDisabled { get; set; }
    }
}
