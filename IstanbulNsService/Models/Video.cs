using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNs.Models
{
    public class Video
    {
        public int Id { get; set; }
        [Required]
        public int AboutUsId { get; set; }
        public string Source { get; set; }
    }
}
