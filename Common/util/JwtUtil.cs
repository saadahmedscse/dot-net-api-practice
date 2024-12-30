using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NoteApp.Core.Domain.Enums;
using NoteApp.Core.Exception;
using NoteApp.Data;

namespace NoteApp.Common.util;

public class JwtUtil(AppDatabaseContext dbContext)
{
    private readonly string JWT_SECRET_KEY = "51c19974a233691d4674f74fc4149062785a4250dbd93081c273b34674317dc2";
    private readonly int TOKEN_EXPIRATION_DAYS = 1; // Customize this value

    public string GenerateAccessToken(string username)
    {
        var claims = new Dictionary<string, object>();
        return GenerateAccessToken(claims, username);
    }

    public bool IsExpired(string token)
    {
        var expirationDate = GetExpirationDateFromToken(token);
        return expirationDate < DateTime.UtcNow;
    }

    public string GetEmail(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
        return jwtToken?.Claims.FirstOrDefault(c => c.Type == "sub")?.Value ?? throw new SecurityTokenException("Invalid token");
    }

    private string GenerateAccessToken(Dictionary<string, object> claims, string username)
    {
        var key = GetSigningKey();
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new JwtSecurityToken(
            issuer: "Saad Ahmed",
            audience: username,
            claims: new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, username),
                new(JwtRegisteredClaimNames.Iss, "Saad Ahmed")
            },
            expires: DateTime.UtcNow.AddDays(TOKEN_EXPIRATION_DAYS),
            signingCredentials: credentials
        );

        foreach (var claim in claims)
        {
            tokenDescriptor.Payload.Add(claim.Key, claim.Value);
        }
        
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(tokenDescriptor);
    }

    private SymmetricSecurityKey GetSigningKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_SECRET_KEY));
    }
    
    private DateTime GetExpirationDateFromToken(string token)
    {
        var claims = GetClaimsFromToken(token);
        var expirationClaim = claims?.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp);
        return expirationClaim != null ? DateTimeOffset.FromUnixTimeSeconds(long.Parse(expirationClaim.Value)).UtcDateTime : DateTime.MinValue;
    }
    
    private IEnumerable<Claim>? GetClaimsFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
        return jwtToken?.Claims;
    }
}