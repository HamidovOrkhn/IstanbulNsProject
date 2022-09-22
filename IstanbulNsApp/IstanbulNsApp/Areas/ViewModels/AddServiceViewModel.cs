using IstanbulNsApp.Resources.DTO;
using IstanbulNsApp.Resources.Form;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Areas.ViewModels
{
    public class AddServiceViewModel
    {
        public List<LangDTO> Langs { get; set; }
        public int IsPayed { get; set; }
        public List<string> Names { get; set; }
        public List<string> Infos { get; set; }
        public int NumberLang { get; set; }
        public IFormFile Picture { get; set; }
    }
}
