using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Resources.DTO
{
    public class DoctorWorkTimes
    {
        public int WorkTimeFromDate { get; set; }
        public int WorkTimeToDate { get; set; }
        public decimal WorkTimeToTime { get; set; }
        public decimal WorkTimeFromTime { get; set; }
    }
}
