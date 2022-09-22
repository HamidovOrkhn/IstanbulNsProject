using IstanbulNsApp.Resources.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Areas.Admin.Models
{
    public class ServiceDTOAdmin
    {
        public int Id { get; set; }
        public int IsPayed { get; set; }
        public List<ServiceNameDTO> ServiceNames { get; set; }
        public List<ServiceInfoDTO> ServiceInfo { get; set; }
        public List<ServicePictureDTO> ServicePictures { get; set; }
    }
}
