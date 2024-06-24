using TestApi.Models;

namespace TestApi.Services.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}
