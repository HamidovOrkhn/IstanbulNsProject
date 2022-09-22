using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Resources.DTO
{
    public class Service
    {

        public int Id { get; set; }
        [Required]      
        [Range(1,10)]
        public int IsPayed { get; set; }
        public List<ServiceName> ServiceNames { get; set; }


    }
}
