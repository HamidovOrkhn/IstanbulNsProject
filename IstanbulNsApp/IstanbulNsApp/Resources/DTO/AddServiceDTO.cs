using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Resources.DTO
{
    public class AddServiceDTO
    {
        public int IsPayed { get; set; }
        public List<ServiceNameDTO> ServiceNames { get; set; }
        public List<ServiceInfoDTO> ServiceInfo { get; set; }
        public ServicePictureDTO ServicePictures { get; set; }
    }
}
