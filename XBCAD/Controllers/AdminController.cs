using Microsoft.AspNetCore.Mvc;
using XBCAD.ViewModels;

namespace XBCAD.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            ViewData["Title"] = "Admin Dashboard";
            return View();
        }

        public IActionResult Users()
        {
            ViewData["Title"] = "Manage Users";
            return View();
        }

        public IActionResult Availability()
        {
            var model = new AvailabilityViewModel
            {
                Days = new List<DayAvailability>
                {
                    new DayAvailability { Day = "Monday", TimeSlots = new List<TimeSlot>() },
                    new DayAvailability { Day = "Tuesday", TimeSlots = new List<TimeSlot>() },
                    new DayAvailability { Day = "Wednesday", TimeSlots = new List<TimeSlot>() },
                    new DayAvailability { Day = "Thursday", TimeSlots = new List<TimeSlot>() },
                    new DayAvailability { Day = "Friday", TimeSlots = new List<TimeSlot>() },
                    new DayAvailability { Day = "Saturday", TimeSlots = new List<TimeSlot>() },
                    new DayAvailability { Day = "Sunday", TimeSlots = new List<TimeSlot>() }
                }
            };
            return View(model);
        }
    }
}
