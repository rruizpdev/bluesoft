using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

#nullable disable
namespace RCRP.Common.Token;

public class JwtTokenRequest
{
    public string Key { get; init; }
    public string Audience { get; init; }
    public string Issuer { get; init; }
    public IEnumerable<Claim> Claims { get; init; } = new List<Claim>();
    public string Algorithm { get; init; } = SecurityAlgorithms.HmacSha256;
    public int MinutesToExpire { get; init; }

    public bool IsValid => !(string.IsNullOrEmpty(Key)
        || string.IsNullOrEmpty(Audience)
        || string.IsNullOrEmpty(Issuer)
        || string.IsNullOrEmpty(Algorithm));
}