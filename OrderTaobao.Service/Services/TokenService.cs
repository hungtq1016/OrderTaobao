
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BaseSource.Dto;
using BaseSource.Model;
using System.Security.Cryptography;

namespace BaseSource.BackendAPI.Services
{
    public interface ITokenService
    {
        bool _isEmptyOrInvalid(string token);
        Task<string> UpdateRefreshToken(TokenRequest request);
        Task<TokenResponse> CreateNewAccessToken(TokenRequest request);
        TokenResponse CreateAccessToken(User user, IList<string> roles);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        string GenerateRefreshToken();
    }
    public class TokenService : ITokenService
    {
        IConfiguration _configuration;
        IAuthenticateRepository _authRepo;
        

        public TokenService(IConfiguration configuration, IAuthenticateRepository authRepo)
        {
            _configuration = configuration;
            _authRepo = authRepo;
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

        //Update refresh token if expire 
        public async Task<string> UpdateRefreshToken(TokenRequest request)
        {
            var principal = GetPrincipalFromExpiredToken(request.AccessToken!);
            string id = principal!.Identity!.Name!;

            if (principal == null || id == null)
            {
                return request.RefreshToken!;
            }

            var user = await _authRepo.ReadUserAsync(id!);
            if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return request.RefreshToken!;
            }

            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            var result = await _authRepo.UpdateUserAsync(user);
            if (!result.Succeeded)
            {
                return request.RefreshToken!;
            }

            return newRefreshToken;
        }

        //Access token expired ---> Get all principal of token ----> New access token
        public async Task<TokenResponse> CreateNewAccessToken(TokenRequest request)
        {
            var principal = GetPrincipalFromExpiredToken(request.AccessToken!);
            string id = principal!.Identity!.Name!;

            var user = await _authRepo.ReadUserAsync(id);
            var roles = await _authRepo.GetRolesByUser(user);
            var refres_token = await UpdateRefreshToken(request);
            user.RefreshToken = refres_token;
            return CreateAccessToken(user, roles);
        }

        //Just create access token
        public TokenResponse CreateAccessToken(User user, IList<string> roles)
        {
            var access_token = AccessToken(user, roles);
            
            return new TokenResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(access_token),
                RefreshToken = user.RefreshToken!,
                TokenType = "Bearer",
                ExpiredAt = user.RefreshTokenExpiryTime
            };
        }

        //Get all principal
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

        //Add all info to claims ----> JWT Token
        private JwtSecurityToken AccessToken(User user,IList<string> roles)
        {
            List<Claim> authClaims = new List<Claim>
            {
                new Claim("Id", user.Id!),
                new Claim(ClaimTypes.Name, user.Id!),
                new Claim("Email", user.Email!),
                new Claim("Expired", DateTime.Now.AddMinutes(ExpiredTime()).ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var role in roles)
            {
                authClaims.Add(new Claim("Role", role));
            }
            //Transform claim to token
            var token = GenerateAccessToken(authClaims);
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

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
