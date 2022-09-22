using IstanbulNs.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNs.Resources
{
    public class ServiceDTO
    {
        public int Id { get; set; }
        [Required]
        [Range(0, 10)]
        public int IsPayed { get; set; }
        public ICollection<ServiceNameDTO> ServiceNames { get; set; }
        public List<ServiceInfoDTO> ServiceInfo { get; set; }
        public ServicePicturesDTO ServicePictures { get; set; }
    }
}
