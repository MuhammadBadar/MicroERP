using QST.MicroERP.Models;

namespace QST.MicroERP.WebAPI.Token
{
    public interface IJWTTokenGenerator
    {
        //string GenerateToken(User user, IList<string> roles, IList<Claim> claims);
        string GenerateToken(User user, IList<string> roles);
    }
}
