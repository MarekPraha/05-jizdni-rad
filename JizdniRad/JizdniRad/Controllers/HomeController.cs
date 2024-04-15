using JizdniRad.Context;
using JizdniRad.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace JizdniRad.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext db;

        public HomeController(DatabaseContext databaseContext)
        {
            db = databaseContext;

        }

        public IActionResult Index()
        {
            var lines = db.lines.ToList();
            List<Stop> lastStops = new List<Stop>();
            

            foreach (var line in lines)
            {
                var lastStop = db.lineStops.Where(x => x.LineID == line.ID).Where(x => x.NextLineStopID == null).Include(x=>x.Stop).FirstOrDefault();
                if (lastStop != null)
                {
                    lastStops.Add(lastStop.Stop);
                }
            }


            this.ViewBag.LastStops = lastStops;
            return View(lines);
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
