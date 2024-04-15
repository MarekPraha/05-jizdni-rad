using JizdniRad.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace JizdniRad.ViewComponents
{
    
    public class SideDepartureViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke(List<LineStop> stops, Line line, LineStop from= null)
        {
            this.ViewBag.from = from;
            this.ViewBag.line = line;
            


            return View(stops);
        }
    }
}
