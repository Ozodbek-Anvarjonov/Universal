using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Project.Application.Common.Identities;
using Project.Application.Settings;
using Project.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project.Infrastructure.Common.Identities;

public class AccessTokenGeneratorService(
    IOptions<JwtSettings> jwtSettings
    ) : IAccessTokenGeneratorService
{
    protected JwtSettings JwtSettings { get; set; } = jwtSettings.Value;

    public Task<string> GenerateAsync(User user, CancellationToken cancellationToken = default)
    {
        var jwtSecurityToken = JwtSecurityToken(user);
        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return Task.FromResult(token);
    }

    private JwtSecurityToken JwtSecurityToken(User user)
    {
        var security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.SecretKey));
        var credentials = new SigningCredentials(security, SecurityAlgorithms.HmacSha256);
        var claims = GetClaims(user);

        return new JwtSecurityToken(
            issuer: JwtSettings.ValidIssuer,
            audience: JwtSettings.ValidAudience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(JwtSettings.AccessTokenLifeTimeInMinutes),
            signingCredentials: credentials
            );
    }

    private IList<Claim> GetClaims(User user)
    {
        return new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
            new Claim(ClaimTypes.Email, user.EmailAddress),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
        };
    }
}