using JizdniRad.Context;
using JizdniRad.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JizdniRad.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly DatabaseContext db;
        public AdministrationController(DatabaseContext databaseContext)
        {
            db = databaseContext;
        }
        public IActionResult Edit(int id)
        {
            var line = db.lines.Where(x => x.ID == id).FirstOrDefault();
            if (line==null)
            {
                return RedirectToAction("Index", "Home");
            }
            var stops = db.lineStops.Where(x=>x.LineID==line.ID).Include(x=>x.Stop).ToList();
            this.ViewBag.stops = stops;


            return View(line);
        }

        [HttpPost]
        public IActionResult Edit(Line line)
        {
            var linedb = db.lines.Where(x => x.ID == line.ID).FirstOrDefault();
            if (line == null)
            {
                return RedirectToAction("Index", "Home");
            }
            linedb.Zone = line.Zone;
            linedb.Name = line.Name;
            db.lines.Update(linedb);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ListStops()
        {
            var stops = db.stops.ToList();
            return View(stops);
        }
        public IActionResult EditStop(int id)
        {
            var stop = db.stops.Where(x => x.ID == id).FirstOrDefault();
            if (stop == null)
            {
                return RedirectToAction("ListStops");
            }
            return View(stop);
        }
        [HttpPost]
        public IActionResult EditStop(Stop stop)
        {
            var stopdb = db.stops.Where(x => x.ID == stop.ID).FirstOrDefault();
            if (stop == null)
            {
                return RedirectToAction("ListStops");
            }
            stopdb.Name = stop.Name;
            db.stops.Update(stopdb);
            db.SaveChanges();

            return RedirectToAction("ListStops");
        }

        public IActionResult CreateStop()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateStop(Stop stop)
        {
            if (stop.Name.Trim()==null||stop.Name.Trim()=="")
            {
                return RedirectToAction("ListStops");

            }
            db.stops.Add(stop);
            db.SaveChanges();
            return RedirectToAction("ListStops");
        }

        //vypnuto kompletně 
        //entityframework kompletně vzdal žití při přidání
        /*
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Line line)
        {
            if (line.Name.Trim()==null|| line.Name.Trim() == ""|| line.Zone.Trim()==null || line.Zone.Trim()=="")
            {
                return RedirectToAction("Index", "Home");
            }
            line.ID = 0;
            Line lineNew = db.lines.Add(line).Entity;
            db.SaveChanges();


            return RedirectToAction("Edit", new {id = lineNew.ID});
        }
        */
    }
}
