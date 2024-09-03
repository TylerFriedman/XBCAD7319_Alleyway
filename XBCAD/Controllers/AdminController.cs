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
            var model = new AvailabilityViewModel();
            return View(model);
        }
    }
}
