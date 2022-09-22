using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNs.Models
{
    public class Home : BaseEntity
    {
        [Required]
        [Display(Name = "Video Link")]
        public string Source { get; set; }
        [Required,MaxLength(100)]
        [Display(Name = "Basliq")]
        public string Title { get; set; }
        [Required, MaxLength(100)]
        [Display(Name = "Metn")]
        public string Text { get; set; }
    }
}
