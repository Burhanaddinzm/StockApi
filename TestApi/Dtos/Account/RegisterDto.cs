using System.ComponentModel.DataAnnotations;

namespace TestApi.Dtos.Account;

/// <summary>
/// Represents a DTO for registering a user.
/// </summary>
public class RegisterDto
{
    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    [Required]
    public string Username { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
