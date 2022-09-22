using IstanbulNsApp.Resources.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.ViewModels
{
    public class OnlineTurnViewModel
    {
        public OnlineTurnDTO OnlineData { get; set; }
        [Display(Prompt = "Şöbə seçin",Name = "Şöbə")]
        public string ServiceName { get; set; }
        [Required]
        public int ServiceId { get; set; }
        [Display(Prompt = "Həkim seçin", Name = "Həkim")]
        public string DoctorName { get; set; }
        [Required]
        public int DoctorId { get; set; }
        [Required(ErrorMessage ="Məlumatı mütləq doldurun")]
        [Display(Prompt = "Tarix seçin", Name = "Tarix")]
        public string Date { get; set; }
        [Required(ErrorMessage = "Məlumatı mütləq doldurun")]
        [Display(Prompt = "Saat seçin", Name = "Saat")]
        public string Hour { get; set; }
        [Required(ErrorMessage = "Məlumatı mütləq doldurun")]
        [MaxLength(256,ErrorMessage ="Maximum şrift limitini keçdiniz")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Telefon formatı düzgün deyil")]
        [Display(Prompt = "Telefon nömrəniz",Name ="Telefon")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Məlumatı mütləq doldurun")]
        [EmailAddress(ErrorMessage ="Email formatı düzgün deyil")]
        [MaxLength(256, ErrorMessage = "Maximum şrift limitini keçdiniz")]
        [Display(Prompt = "Emailiniz", Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Məlumatı mütləq doldurun")]
        [Display(Prompt = "Mövzu", Name = "Mövzu haqqında qısa məlumat")]
        public string Subject { get; set; }
        [Display(Prompt = "Əlavə məlumat", Name = "Əlavə məlumat")]
        public string Info { get; set; }
        [Display(Prompt = "Qiymət", Name = "Qiymət")]
        public int Price { get; set; }
    }
}
