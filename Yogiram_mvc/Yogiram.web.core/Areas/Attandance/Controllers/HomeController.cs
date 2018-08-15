using Attandance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yogiram.core.Mvc;

namespace Attandance.Controllers
{
    public class HomeController : AttandanceController
    {
        [MenuAuthorize("D79DB074-8067-4084-A03A-E194F5A5827E")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CalendarBind(string start, string end)
        {
            var dates = new List<DateTime>();
            List<CalendarModels> wholeMonth = new List<CalendarModels>();
            int month = Convert.ToDateTime(start).Day == 1 ? Convert.ToDateTime(start).Month : Convert.ToDateTime(start).Month + 1;

            for (var date = new DateTime(DateTime.Now.Year, month, 1); date.Month == month; date = date.AddDays(1))
                dates.Add(date);

            List<CalendarModels> events = (from a in CoreContext.EmployeeMaster.AsEnumerable()
                                           join b in CoreContext.DeviceLogs.AsEnumerable() on a.EmployeeId equals b.EmpId
                                           group b by b.PunchDateTime.ToString("yyyy-MM-dd") into g
                                           let InTime = (from ai in g select ai.PunchDateTime.ToString("hh:mm tt")).OrderBy(a => a).FirstOrDefault()
                                           let OutTime = g.Count() == 1 ? "-" : (from ai in g select ai.PunchDateTime.ToString("hh:mm tt")).OrderByDescending(a => a).FirstOrDefault()
                                           orderby g.Key descending
                                           select new CalendarModels
                                           {
                                               title = "IN - " + InTime + " | OUT - " + OutTime,
                                               start = g.Key,
                                               end = g.Key
                                           }).ToList();

            foreach (var item in dates)
            {
                var IsDate = (from a in events where a.start.Contains(item.ToString("yyyy-MM-dd")) select a).Any();
                if (!IsDate)
                    wholeMonth.Add(new CalendarModels { title = "Unpunch", start = item.ToString("yyyy-MM-dd"), end = item.ToString("yyyy-MM-dd"), className = "event-azure" });
            }

            events.AddRange(wholeMonth);
            var EventRows = events.ToArray();

            return Json(EventRows, JsonRequestBehavior.AllowGet);

        }

    }
}