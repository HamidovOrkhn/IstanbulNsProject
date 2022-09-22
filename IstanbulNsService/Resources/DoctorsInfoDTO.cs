using IstanbulNs.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNs.Resources
{
    public class DoctorsInfoDTO
    {
        public int Id { get; set; }
        [Required]
        public int DoctorId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Email { get; set; }
        [Required]
        [MaxLength(400)]
        public string Location { get; set; }
        [Required]
        [Range(0, 10)]
        public int WorkTimeFromDate { get; set; }
        [Required]
        [Range(0, 10)]
        public int WorkTimeToDate { get; set; }
        [Required]
        [Range(0, 30)]
        public decimal WorkTimeFromTime { get; set; }
        [Required]
        [Range(0, 30)]
        public decimal WorkTimeToTime { get; set; }
        public ICollection<PhoneDocDTO> Phones { get; set; }
    }
}
