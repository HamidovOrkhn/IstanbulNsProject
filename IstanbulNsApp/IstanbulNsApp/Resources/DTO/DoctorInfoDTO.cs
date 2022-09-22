using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Resources.DTO
{
    public class DoctorInfoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sex { get; set; }
        public string Picture { get; set; }
        public int LevelType { get; set; }
        public string DoctorLevel { get; set; }
        public string Info { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public List<Phone> Phones { get; set; }
        public int WorkTimeFromDate { get; set; }
        public int WorkTimeToDate { get; set; }
        public decimal WorkTimeToTime { get; set; }
        public decimal WorkTimeFromTime { get; set; }
    }
}
