using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Areas.Admin.Models
{
    public class InfoStr
    {
        public int DoctorId { get; set; }
        public string Info { get; set; }
        public int Id { get; set; }
        public int LangId { get; set; }
        public object Lang { get; set; }
    }
}
