using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlogPessoal.Services.Token;

public interface ITokenService
{
    JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims,
                                         IConfiguration _config);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token,
                                                 IConfiguration _config);
}
