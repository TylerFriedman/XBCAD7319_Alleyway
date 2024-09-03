using System.ComponentModel.DataAnnotations;

namespace XBCAD.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Token { get; set; } // If using Firebase token authentication
    }
}
