using Microsoft.AspNetCore.Mvc;
using XBCAD.ViewModels;
using System.Collections.Generic;

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

        // Static instance to store the availability data
        private static AvailabilityViewModel savedAvailability = new AvailabilityViewModel
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

        public IActionResult Availability()
        {
            return View(savedAvailability);
        }

        [HttpPost]
        public IActionResult SaveAvailability(AvailabilityViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Update savedAvailability with the data from the model
                foreach (var incomingDay in model.Days)
                {
                    var savedDay = savedAvailability.Days.FirstOrDefault(d => d.Day == incomingDay.Day);
                    if (savedDay != null)
                    {
                        // Ensure the TimeSlots are not null
                        if (incomingDay.TimeSlots != null)
                        {
                            savedDay.TimeSlots = incomingDay.TimeSlots;
                        }
                    }
                }
            }
            // Return the view with the updated savedAvailability to ensure data persistence
            return View("Availability", savedAvailability);
        }
    }






}
