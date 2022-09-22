using IstanbulNsApp.Resources.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IstanbulNsApp.Areas.Admin.Models
{
    public class Statistics
    {
        public int AllQueryNumber { get; set; }
        public int OnlineQueryNumber { get; set; }
        public int RealQueryNumber { get; set; }
        public int PriceToday { get; set; }
        public int PriceMonth { get; set; }
        public List<OnlineQueryDTO> TodayOnline { get; set; }
        public List<OnlineQueryDTO> MonthOnline { get; set; }
        public List<OnlineQueryDTO> TodayReal { get; set; }
        public List<OnlineQueryDTO> MonthReal { get; set; }
    }
}
