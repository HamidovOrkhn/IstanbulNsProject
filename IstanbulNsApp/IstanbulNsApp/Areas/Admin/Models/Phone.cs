using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Areas.Admin.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
