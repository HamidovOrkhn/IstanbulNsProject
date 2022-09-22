using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Areas.Admin.Models
{
    public class LoginResp
    {
        public string jwtToken { get; set; }
        public UserData userData { get; set; }
    }
}
