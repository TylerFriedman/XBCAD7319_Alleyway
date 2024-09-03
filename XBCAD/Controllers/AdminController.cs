using Microsoft.AspNetCore.Mvc;
using XBCAD.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace XBCAD.Controllers
{
    public class AdminController : Controller
    {
        // Static list to store the availability data
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
            return View(savedAvailability);
        }

        [HttpPost]
        public IActionResult SaveAvailability(AvailabilityViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Update the saved availability with the new model
                savedAvailability = model;

                // Return the view with the updated model
                return View("Availability", savedAvailability);
            }

            // If there are validation errors, re-display the form with validation messages
            return View("Availability", model);
        }
    }
}
