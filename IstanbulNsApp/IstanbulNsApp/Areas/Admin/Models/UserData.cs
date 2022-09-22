using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Areas.Admin.Models
{
    public class UserData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int sexId { get; set; }
        public object password { get; set; }
        public string token { get; set; }
        public int isDisabled { get; set; }
        public int role { get; set; }
    }
}
