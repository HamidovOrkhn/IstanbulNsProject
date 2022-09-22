using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Areas.Admin.Models
{
    public class DoctorsInfo
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public int WorkTimeFromDate { get; set; }
        public int WorkTimeToDate { get; set; }
        public decimal WorkTimeFromTime { get; set; }
        public decimal WorkTimeToTime { get; set; }
        public List<Phone> Phones { get; set; }
    }
}
