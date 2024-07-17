using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Papirus.Tests.Common.Security;

[ExcludeFromCodeCoverage]
public static class TokenManager
{
    public static string GenerateToken(IConfiguration configuration, User user)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtBearer:SecretKey"]!));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var header = new JwtHeader(signingCredentials);

        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new (JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new (JwtRegisteredClaimNames.Email, user.Email),
            new ("name", $"{user.FirstName} {user.LastName}"),
            new ("firmId", user.FirmId.ToString()),
            new ("roleId", user.RoleId.ToString())
        };

        // Create the PayLoad
        var payload = new JwtPayload(
            issuer: configuration["JwtBearer:Issuer"],
            audience: configuration["JwtBearer:Audience"],
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTime.UtcNow.AddHours(24)
        );

        // Generate the Token
        var token = new JwtSecurityToken(header, payload);
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        return jwtSecurityTokenHandler.WriteToken(token);
    }

    public static JwtPayload DecodeToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token);
        var tokenS = jsonToken as JwtSecurityToken;

        return tokenS!.Payload;
    }
}