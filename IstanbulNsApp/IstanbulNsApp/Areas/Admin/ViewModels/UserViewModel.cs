using IstanbulNsApp.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Areas.Admin.ViewModels
{
    public class UserViewModel:User
    {
        [Required(ErrorMessage = "Sahə doldurulmalıdır")]
        [MaxLength(200, ErrorMessage = "Maximum Şrift sayını keçdiniz")]
        [MinLength(6, ErrorMessage = "Minimum 6 Şrift olmalıdır")]
        [Compare("Password",ErrorMessage ="Kod təkrarı kod ilə eyni deyil")]
        public string ConfirmPassword { get; set; }
    }
}
