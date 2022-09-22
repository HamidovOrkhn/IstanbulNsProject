using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNs.Models
{
    public class Lang
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Key { get; set; }
        public ICollection<DoctorsInfo> DoctorsInfos { get; set; }
        public ICollection<ServiceName> ServiceNames { get; set; }
        public ICollection<ServiceInfo> ServiceInfos { get; set; }


    }
}
