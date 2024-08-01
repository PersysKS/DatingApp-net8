using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDto
{
    [Required(ErrorMessage = "Username is required.")]
    [MinLength(3, ErrorMessage = "Username must be at least 3 characters long.")]
    public  string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Passowrd is required.")]
    public  string Password { get; set; } = String.Empty;
}
