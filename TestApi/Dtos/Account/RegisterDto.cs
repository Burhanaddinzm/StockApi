using System.ComponentModel.DataAnnotations;

namespace TestApi.Dtos.Account;

public class RegisterDto
{
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
