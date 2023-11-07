
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BaseSource.Dto;
using BaseSource.Model;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;

namespace BaseSource.BackendAPI.Services
{
    public interface ITokenService
    {
        bool _isEmptyOrInvalid(string token);
        Task<string> UpdateRefreshToken(TokenRequest request);
        Task<TokenResponse> CreateNewAccessToken(TokenRequest request);
        Task<TokenResponse> CreateAccessToken(User user, IList<string> roles);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
    public class TokenService : ITokenService
    {
        IConfiguration _configuration;
        UserManager<User> _userManager;
        RoleManager<Role> _roleManager;

        public TokenService(IConfiguration configuration, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public bool _isEmptyOrInvalid(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return true;
            }

            var jwtToken = new JwtSecurityToken(token);
            return (jwtToken == null) || (jwtToken.ValidFrom > DateTime.UtcNow) || (jwtToken.ValidTo < DateTime.UtcNow);
        }

        public async Task<string> UpdateRefreshToken(TokenRequest request)
        {
            var principal = GetPrincipalFromExpiredToken(request.AccessToken!);
            string id = principal!.Identity!.Name!;

            if (principal == null || id == null)
            {
                return request.RefreshToken!;
            }

            var user = await _userManager.FindByIdAsync(id!);
            if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return request.RefreshToken!;
            }

            string newRefreshToken = TokenHelper.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return request.RefreshToken!;
            }

            return newRefreshToken;
        }

        public async Task<TokenResponse> CreateNewAccessToken(TokenRequest request)
        {
            var principal = GetPrincipalFromExpiredToken(request.AccessToken!);
            string id = principal!.Identity!.Name!;

            User? user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            var refres_token = await UpdateRefreshToken(request);
            user.RefreshToken = refres_token;
            return  await CreateAccessToken(user, roles);
        }

        public async Task<TokenResponse> CreateAccessToken(User user, IList<string> roles)
        {
            var access_token = await AccessToken(user, roles);
            
            return new TokenResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(access_token),
                RefreshToken = user.RefreshToken!,
                TokenType = "Bearer",
                ExpiredAt = user.RefreshTokenExpiryTime
            };
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }

        private async Task<JwtSecurityToken> AccessToken(User user,IList<string> roles)
        {
            IdentityOptions _options = new IdentityOptions();

            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id!),
                new Claim(_options.ClaimsIdentity.UserIdClaimType, user.Id.ToString()),
                new Claim(_options.ClaimsIdentity.UserNameClaimType, user.UserName),
            };

            claims.AddRange(userClaims);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach (Claim roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }
            //Transform claim to token
            var token = GenerateAccessToken(claims);
            return token;
        }

        //Claims
        private JwtSecurityToken GenerateAccessToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));


            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(ExpiredTime()),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }


        private int ExpiredTime()
        {
            _ = int.TryParse(_configuration["JWT:AccessTokenValidityInMinutes"], out int accessTokenValidityInMinutes);
            return accessTokenValidityInMinutes;
        }

    }
}
