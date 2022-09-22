using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNs.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(400)]
        public string Name { get; set; }    
        [Required]
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public string? Picture { get; set; }
        public int LevelType { get; set; }
        [Required]
        [MaxLength(200)]
        public string DoctorLevel { get; set; }
        [Range(0,5)]
        public int Sex { get; set; }
        public int Price { get; set; }
        public ICollection<OnlineQuery> OnlineQueries { get; set; }
        public ICollection<DoctorsInfo> DoctorsInfos { get; set; }
        public ICollection<InfoStr> InfoStrs { get; set; }
    }
}
