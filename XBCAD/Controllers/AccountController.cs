using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Mvc;
using XBCAD.Models;
using XBCAD.ViewModels;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Text.Json; // For JSON serialization
using Newtonsoft.Json.Linq;  // Ensure this is included


namespace XBCAD.Controllers
{
    public class AccountController : Controller
    {
        private readonly FirebaseAuth auth;
        private readonly HttpClient httpClient;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClientFactory.CreateClient();

            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.GetApplicationDefault(),
                });
            }
            this.auth = FirebaseAuth.DefaultInstance;
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userRecord = await this.auth.CreateUserAsync(new UserRecordArgs
                    {
                        Email = model.Username,
                        Password = model.Password,
                        DisplayName = $"{model.FirstName} {model.LastName}",
                        Disabled = false,
                    });

                    // Prepare data to be saved in RTDB
                    var data = new { role = "client" };
                    var json = JsonSerializer.Serialize(data);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    // Construct the URL to Firebase RTDB
                    var url = $"https://alleysway-310a8-default-rtdb.firebaseio.com/users/{userRecord.Uid}.json";

                    // Send data to Firebase RTDB
                    var response = await httpClient.PutAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        throw new Exception("Failed to save user role to database.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Registration failed: {ex.Message}");
                    return View(model);
                }
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) //i need to change this back and fix it
            {
                try
                {
                    // Commenting out the Firebase verification and RTDB logic
                    /*
                    var decodedToken = await auth.VerifyIdTokenAsync(model.Token);
                    var uid = decodedToken.Uid;

                    // Fetch user role from Firebase RTDB
                    var url = $"https://alleysway-310a8-default-rtdb.firebaseio.com/users/{uid}.json";
                    var response = await httpClient.GetStringAsync(url);
                    var data = JObject.Parse(response);
                    var role = data["role"]?.ToString();
                    
                    // Redirect based on role
                    if (role == "admin")
                    {
                        return RedirectToAction("AdminDashboard", "Dashboard");
                    }
                    else if (role == "client")
                    {
                        return RedirectToAction("ClientDashboard", "Dashboard");
                    }
                    else
                    {
                        throw new Exception("Role not found or unauthorized.");
                    }
                    */

                    // Workaround based on the entered username
                    if (model.Username.Equals("admin", StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }
                    else if (model.Username.Equals("client", StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction("Dashboard", "Client");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid username or role not found.");
                        return View(model);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Login failed: {ex.Message}");
                    return View(model);
                }
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Login");
        }
    }
}
