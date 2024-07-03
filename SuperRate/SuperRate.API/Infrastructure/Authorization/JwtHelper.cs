using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SuperRate.Application.Users.Responses;

namespace SuperRate.API.Infrastructure.Authorization;

public static class JwtHelper
{
    public static string GenerateToken(UserResponseModel user, IConfiguration config)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["AuthConfiguration:SecretKey"]!));

        var issuer = config["AuthConfiguration:Issuer"];
        var audience = config["AuthConfiguration:Audience"];
        var exp = double.Parse(config["AuthConfiguration:ExpInMinutes"]!);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var token = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.UtcNow.AddMinutes(exp),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }
}