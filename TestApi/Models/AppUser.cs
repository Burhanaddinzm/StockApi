using Microsoft.AspNetCore.Identity;

namespace TestApi.Models;

public class AppUser : IdentityUser
{
    public List<Portfolio> Portfolios { get; set; }
    public AppUser()
    {
        Portfolios = new List<Portfolio>();
    }

    //public override bool LockoutEnabled { get => true; set => base.LockoutEnabled = value; }
}
