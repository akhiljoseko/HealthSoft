using System.ComponentModel.DataAnnotations;

public class LoginRequestDto
{
    [Required(ErrorMessage = "Email is required")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Email is required")]
    public  required string Password { get; set; }
}
