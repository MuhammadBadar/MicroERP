using MicroERP.Models;

namespace MicroERP.WebAPI.Token
{
    public interface IJWTTokenGenerator
    {
        //string GenerateToken(User user, IList<string> roles, IList<Claim> claims);
        string GenerateToken(User user, IList<string> roles);
    }
}
