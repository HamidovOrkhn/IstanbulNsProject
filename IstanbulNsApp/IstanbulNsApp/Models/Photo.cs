using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Models
{
    public class Photo
    {
        public int Id { get; set; }
        [Required]
        public int AboutUsId { get; set; }
        public string Name { get; set; }
    }
}
