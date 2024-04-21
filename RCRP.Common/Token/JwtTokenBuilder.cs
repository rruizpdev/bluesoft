using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

#nullable disable
namespace RCRP.Common.Token;

public static class JwtTokenBuilder
{
    public static JwtToken Build(JwtTokenRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        if (!request.IsValid)
            throw new ArgumentException("Invalid token request");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(request.Key));
        var credentials = new SigningCredentials(securityKey, request.Algorithm);
        var token = new JwtSecurityToken(request.Issuer,
              request.Audience,
              request.Claims,
              expires: DateTime.UtcNow.AddMinutes(request.MinutesToExpire),
              signingCredentials: credentials);

        return new JwtToken(token);
    }
}
