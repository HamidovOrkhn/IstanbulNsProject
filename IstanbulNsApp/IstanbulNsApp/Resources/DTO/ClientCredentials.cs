using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Resources.DTO
{
    public class ClientCredentials
    {
        [Required(ErrorMessage = "Məlumatı mütləq doldurun")]
        [Phone(ErrorMessage = "Telefon formatı düzgün deyil")]
        [MaxLength(256, ErrorMessage = "Maximum şrift limitini keçdiniz")]
        [Display(Prompt = "(050) 611-22-33", Name = "Telefon")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Məlumatı mütləq doldurun")]
        [Display(Prompt = "Sıra kodunuz", Name = "Sıra kodunuz")]
        public int QueryId { get; set; }
    }
}
