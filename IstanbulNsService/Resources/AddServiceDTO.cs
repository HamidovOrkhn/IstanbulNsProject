using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNs.Resources
{
    public class AddServiceDTO
    {
        public int IsPayed { get; set; }
        public ICollection<ServiceNameDTO> Names { get; set; }
        public List<ServiceInfoDTO> Infos { get; set; }
        public string Picture { get; set; }
    }
}
