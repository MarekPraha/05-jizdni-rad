using JizdniRad.Models;
using Microsoft.AspNetCore.Mvc;

namespace JizdniRad.ViewComponents
{
    public class DepartureRowViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(List<Departure> departures)
        {
            return View(departures);
        }
    }
}
