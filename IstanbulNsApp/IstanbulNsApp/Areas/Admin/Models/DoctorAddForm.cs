using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Areas.Admin.Models
{
    public class DoctorAddForm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ServiceId { get; set; }
        public object Service { get; set; }
        public IFormFile Picture { get; set; }
        public int LevelType { get; set; }
        public string DoctorLevel { get; set; }
        public int Sex { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public int WorkTimeFromDate { get; set; }
        public int WorkTimeToDate { get; set; }
        public decimal WorkTimeFromTime { get; set; }
        public decimal WorkTimeToTime { get; set; }
        public List<string> Phones { get; set; }
        public List<string> InfoStrs { get; set; }
        public string ExistedPicture { get; set; }
    }
}
