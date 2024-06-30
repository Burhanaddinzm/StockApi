using Microsoft.AspNetCore.Identity;

namespace TestApi.Models;

/// <summary>
/// Represents a user of the application.
/// </summary>
/// <remarks>This class inherits from <see cref="BaseAuditableEntity"/>.</remarks>
public class AppUser : IdentityUser
{
    /// <summary>
    /// The list of portfolios owned by the user.
    /// </summary>
    public List<Portfolio> Portfolios { get; set; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="AppUser"/> class.
    /// </summary>
    public AppUser()
    {
        Portfolios = new List<Portfolio>();
    }

    ///// <summary>
    ///// Gets or sets a value indicating whether the user is allowed to be locked out.
    ///// </summary>
    //public override bool LockoutEnabled { get => true; set => base.LockoutEnabled = value; }
}
