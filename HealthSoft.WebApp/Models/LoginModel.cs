using System.ComponentModel.DataAnnotations;

namespace HealthSoft.WebApp.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public required string EmailId { get; set; }

        [Required(ErrorMessage ="Password is required")]
        public required string Password { get; set; }
    }
}
