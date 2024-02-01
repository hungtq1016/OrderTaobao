using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OAuth2Service.DTOs;
using OAuth2Service.Infrastructure.Data;
using OAuth2Service.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OAuth2Service.Services;

public interface ITokenService
{
    Task<TokenResponse> GetTokenResponseAsync(User user);
    Task<string> GetAccessToken(User user);
    Task<string> GetRefreshToken(User user);

}

public class TokenService : ITokenService
{
    private IConfiguration _config;
    private readonly OAuth2Context _context;
    public TokenService(IConfiguration config, OAuth2Context context)
    {
        _config = config;
        _context = context;
    }

    public async Task<TokenResponse> GetTokenResponseAsync(User user)
    {
        return new TokenResponse
        {
            AccessToken = await GetAccessToken(user),
            ExpiredAt = DateTime.Now.AddMinutes(ExpiredTime()),
            RefreshToken = Guid.NewGuid().ToString(),
            TokenType = "Beared"
        };
    }

    public async Task<string> GetAccessToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.FullName),
        };

        var permissions = await LoadPermissionsFromDb(user.Id);

        foreach (var permission in permissions)
        {
            claims.Add(new Claim(permission.Type, permission.Value)); // Assuming 'Name' is a property of 'Permission'
        }

        // Generate the access token with the claims
        return AccessTokenGenerator(claims);

    }

    public Task<string> GetRefreshToken(User user)
    {
        throw new NotImplementedException();
    }

    private string AccessTokenGenerator(List<Claim> claims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]!));


        var token = new JwtSecurityToken
            (
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(ExpiredTime()),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    private async Task<List<Permission>> LoadPermissionsFromDb(Guid userId)
    {
        return await _context.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Groups)
            .SelectMany(g => g.Role.Assignments)
            .Select(a => a.Permission)
            .Distinct()
            .ToListAsync();
    }

    private int ExpiredTime() => int.TryParse(_config["JWT:AccessTokenValidityInMinutes"], out int accessTokenValidityInMinutes) ? accessTokenValidityInMinutes : 0;

}
