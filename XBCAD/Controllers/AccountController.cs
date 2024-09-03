using Microsoft.AspNetCore.Mvc;
using XBCAD.ViewModels;

namespace XBCAD.Controllers
{
    //test
    public class AccountController : Controller
    {
        // GET: Account/Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Example: Check user role
                if (model.Username == "admin") // Replace with actual role checking logic
                {
                    // Redirect to Admin Dashboard
                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                {
                    // Redirect to Client Dashboard
                    return RedirectToAction("Dashboard", "Client");
                }
            }
            return View(model);
        }
        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }
         public IActionResult Dashboard()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Handle registration logic here
                return RedirectToAction("Login");
            }
            return View(model);
        }
        public IActionResult Logout()
        {
            // Log out logic
            return RedirectToAction("Login");
        }
    }
}

