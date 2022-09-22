using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNs.Models
{
    public class AboutUs :BaseEntity
    {
        [Required, MaxLength(100)]
        [Display(Name = "Metn")]
        //[Column(TypeName = "ntext")]
        public string Text { get; set; }
        [Required]
        [Display(Name = "Youtube Link")]
        public string YoutubeSource { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public Video Videos { get; set; }
    }
}
