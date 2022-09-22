using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Areas.Admin.Models
{
    public class DoctorAdd
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ServiceId { get; set; }
        public object Service { get; set; }
        public object Picture { get; set; }
        public int LevelType { get; set; }
        public string DoctorLevel { get; set; }
        public int Sex { get; set; }
        public object OnlineQueries { get; set; }
        public DoctorsInfo DoctorsInfos { get; set; }
        public List<InfoStr> InfoStrs { get; set; }
    }
}
