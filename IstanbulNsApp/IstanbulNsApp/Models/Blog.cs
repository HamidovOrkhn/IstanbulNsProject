using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Sekil")]
        public string Photo { get; set; }
        public IFormFile PhotoUpload { get; set; }
        public DateTime Date { get; set; }
        [Required, MaxLength(100)]
        [Display(Name = "Basliq")]
        public string Title { get; set; }
        [Required, MaxLength(500)]
        [Display(Name = "Metn")]
        public string Text { get; set; }
    }
}
