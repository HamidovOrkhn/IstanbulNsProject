using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IstanbulNs.Data;
using IstanbulNs.Models;
using IstanbulNs.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IstanbulNs.Controllers
{
    [Route("api/statistics")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IndexContext _db;
        public StatisticsController
        (
            IndexContext db
        )
        {
            _db = db;
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAdminStatistics()
        {
            DateTime today = DateTime.Now.Date;
            List<OnlineQuery> onlineQueryAll = await _db.OnlineQueries.ToListAsync();

            IEnumerable<OnlineQuery> todayqueriesOnline = onlineQueryAll.Where(a => a.QueryDate.Date > today.AddDays(-1) && a.QueryDate.Date <= today && a.IsDeleted==0 && a.IsOnline ==1);

            IEnumerable<OnlineQuery> todayqueriesReal = onlineQueryAll.Where(a => a.QueryDate.Date > today.AddDays(-1) && a.QueryDate.Date <= today && a.IsDeleted == 0 && a.IsOnline == 0);

            IEnumerable<OnlineQuery> monthqueriesOnline = onlineQueryAll.Where(a => a.QueryDate.Month > today.AddMonths(-1).Month && a.QueryDate.Month <= today.Month && a.IsOnline == 1);

            IEnumerable<OnlineQuery> monthqueriesReal = onlineQueryAll.Where(a => a.QueryDate.Month > today.AddMonths(-1).Month && a.QueryDate.Month <= today.Month && a.IsOnline == 0);
            int online = onlineQueryAll.Where(a => a.IsOnline == 1 && a.IsDeleted == 0).Count();
            int all = onlineQueryAll.Where(a => a.IsDeleted == 0).Count();
            var data = new
            {
                AllQueryNumber = all,
                OnlineQueryNumber = online,
                RealQueryNumber = all-online,
                TodayOnline = todayqueriesOnline,
                MonthOnline = monthqueriesOnline,
                TodayReal = todayqueriesReal,
                MonthReal = monthqueriesReal,
                PriceToday = onlineQueryAll.Where(a => a.QueryDate.Date > today.AddDays(-1) && a.QueryDate.Date <= today).Sum(a=>a.Price),
                PriceMonth = onlineQueryAll.Where(a => a.QueryDate.Month > today.AddMonths(-1).Month && a.QueryDate.Month <= today.Month && a.IsOnline == 1).Sum(a => a.Price)

            };
            return Ok(new ReturnMessage(data:data));

        }
    }
}
