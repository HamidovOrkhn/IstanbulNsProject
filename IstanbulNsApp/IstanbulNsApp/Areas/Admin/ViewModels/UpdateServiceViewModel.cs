using IstanbulNsApp.Areas.Admin.Models;
using IstanbulNsApp.Resources.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Areas.Admin.ViewModels
{
    public class UpdateServiceViewModel
    {
        public int Id { get; set; }
        public ServiceDTOAdmin Services { get; set; }
        public int NumberLang { get; set; }
        public List<LangDTO> Langs { get; set; }
        public int IsPayed { get; set; }
        public List<string> Names { get; set; }
        public List<string> Infos { get; set; }
        public IFormFile Picture { get; set; }
    }
}
