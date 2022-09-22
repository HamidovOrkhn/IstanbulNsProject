
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Resources.DTO
{
    public class OnlineQuery
    {
        public int Id { get; set; }
        [Required]
        public int ServiceId { get; set; }
        [Required]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime ServeDate { get; set; }
        [Required]
        public DateTime QueryDate { get; set; }
        [Required]
        [MaxLength(256)]
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(256)]
        public string Info { get; set; }
        [Required]
        [MaxLength(256)]
        public string Subject { get; set; }
        [Required]
        [MaxLength(256)]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(50)]
        public string Code { get; set; }
        [Range(0, 10)]
        public int IsSchedule { get; set; }
        [Range(0, 10)]
        public int IsDeleted { get; set; }
        public int Price { get; set; }
        public Pdf Pdf { get; set; }


    }
}
