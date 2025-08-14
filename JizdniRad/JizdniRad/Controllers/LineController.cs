using JizdniRad.Context;
using JizdniRad.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace JizdniRad.Controllers
{
    public class LineController : Controller
    {

        private readonly DatabaseContext db;

        public LineController(DatabaseContext databaseContext)
        {
            db = databaseContext;

        }
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Detail(int id, int? stopIDfrom = null)
        {
            Line? line = db.lines.Include(x=>x.FirstStop).ThenInclude(x=>x.Stop).Where(x=>x.ID==id).FirstOrDefault();
            int offset = 0;
            LineStop? lineStopfrom = null;
            if (line==null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (stopIDfrom!=null)
            {
                lineStopfrom = db.lineStops.Where(x => x.LineID == id && x.ID == stopIDfrom).FirstOrDefault();
                if (lineStopfrom==null)
                {
                    return RedirectToAction("Detail",new {id=line.ID});
                }
                var current = db.lineStops.Where(x => x.ID == line.FirstStopID).FirstOrDefault();

                var linestopsLocal = db.lineStops.Where(x => x.LineID == line.ID).ToList();

                while (current.ID != lineStopfrom.ID)
                {
                    offset += current.TimeToNextStop ?? 0;
                    current = linestopsLocal.Where(x => x.ID == current.NextLineStopID).FirstOrDefault();

                    if (current == null)
                    {
                        break;
                    }
                    //Debug.WriteLine(current.ID);
                }
             
                this.ViewBag.offset = offset;
                this.ViewBag.from = lineStopfrom;
            }
            
            

            List<LineStop> stops = new List<LineStop>();
            stops.Add(db.lineStops.Include(x=>x.Stop).Include(x=>x.Line).Where(x => x.ID == line.FirstStopID).FirstOrDefault());

            List<LineStop> lineStopLocal = db.lineStops.Include(x=>x.Stop).Include(x=>x.Line).Where(x => x.LineID == line.ID).ToList();

            while (stops.Last().NextLineStopID!=null)
            {
                stops.Add(lineStopLocal.Where(x => x.ID == stops.Last().NextLineStopID).FirstOrDefault());
            }
            this.ViewBag.Stops = stops;


            var depratures = db.departures.Where(x => x.LineID == line.ID).ToList();
            foreach (var item in depratures)
            {
                item.Time += TimeSpan.FromMinutes(offset);
            }
            this.ViewBag.depratures = depratures;

            return View(line);  
        }
    }
}
