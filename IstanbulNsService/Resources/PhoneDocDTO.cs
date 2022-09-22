using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNs.Resources
{
    public class PhoneDocDTO
    {
        public int Id { get; set; }
        [Required]
        public int DoctorId { get; set; }
        [Required]
        [Phone]
        [MaxLength(200)]
        public string PhoneNumber { get; set; }
    }
}
