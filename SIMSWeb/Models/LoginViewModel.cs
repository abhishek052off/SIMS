using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SIMSWeb.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter a valid Email.")]
        [EmailAddress]
        public string Email { get; set; }
        [DisplayName("Display Order")]
        public string Password { get; set; }

    }
}
