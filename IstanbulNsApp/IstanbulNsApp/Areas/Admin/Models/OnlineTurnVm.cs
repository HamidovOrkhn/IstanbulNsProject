using IstanbulNsApp.Resources.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Areas.Admin.Models
{
    public class OnlineTurnVm
    {
        public List<OnlineQuery> queries { get; set; }
        public int page { get; set; }
        public IFormFile File { get; set; }
        public int query_id { get; set; }
    }
}
