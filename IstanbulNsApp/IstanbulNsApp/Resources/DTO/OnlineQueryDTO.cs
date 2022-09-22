using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Resources.DTO
{
    public class OnlineQueryDTO
    {
        [Required]
        public int? ServiceId { get; set; }
        [Required]
        public int? DoctorId { get; set; }
        public DateTime? ServeDate { get; set; }
        [Required]
        public DateTime? QueryDate { get; set; }
        [Required]
        [MaxLength(256)]
        public string Info { get; set; }
        [Required]
        [MaxLength(256)]
        public string Subject { get; set; }
        [Required]
        [MaxLength(256)]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [MaxLength(256)]
        [Phone]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Code { get; set; }
        public int Price { get; set; }

    }
}
