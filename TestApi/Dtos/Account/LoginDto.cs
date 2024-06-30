using System.ComponentModel.DataAnnotations;

namespace TestApi.Dtos.Account;

/// <summary>
/// A data transfer object for login information.
/// </summary>
public class LoginDto
{
    /// <summary>
    /// The user name.
    /// </summary>
    [Required]
    public string UserName { get; set; } = null!;

    /// <summary>
    /// The password.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
