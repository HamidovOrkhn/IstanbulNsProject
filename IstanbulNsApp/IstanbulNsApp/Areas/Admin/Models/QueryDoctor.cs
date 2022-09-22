using IstanbulNsApp.Resources.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Areas.Admin.Models
{
    public class QueryDoctor:Doctor
    {
        public int? Price { get; set; }
    }
}
