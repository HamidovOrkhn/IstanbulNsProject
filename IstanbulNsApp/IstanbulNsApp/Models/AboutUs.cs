using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Models
{
    public class AboutUs
    {
        [Required, MaxLength(800)]
        [Display(Name = "Metn")]
        public string Text { get; set; }
        [Required]
        [Display(Name = "Youtube Link")]
        public string YoutubeSource { get; set; }
        public List<Photo> Photos { get; set; }
        public Video Videos { get; set; }
    }
}
