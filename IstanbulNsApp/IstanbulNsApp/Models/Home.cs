using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Models
{
    public class Home
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Video Link")]
        public string Source { get; set; }
        [NotMapped]
        public IFormFile VideoUpload { get; set; }
        [Required, MaxLength(100)]
        [Display(Name = "Basliq")]
        public string Title { get; set; }
        [Required, MaxLength(100)]
        [Display(Name = "Metn")]
        public string Text { get; set; }
    }
}
