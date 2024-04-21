using System.IdentityModel.Tokens.Jwt;

namespace RCRP.Common.Token;

public sealed class JwtToken
{
    private readonly JwtSecurityToken _token;

    public JwtToken(JwtSecurityToken token)
    {
        _token = token;
    }

    public DateTime ValidThru => _token.ValidTo;
    public string Token => new JwtSecurityTokenHandler()
        .WriteToken(_token);
}
