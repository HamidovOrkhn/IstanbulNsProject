using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Resources.DTO
{
    public class ServiceInfoDTO
    {
        public int Id { get; set; }
        public int LangId { get; set; }
        public string Name { get; set; }
        [Required]
        public int ServiceId { get; set; }
    }
}
