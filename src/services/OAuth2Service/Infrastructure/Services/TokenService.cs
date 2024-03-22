namespace OAuth2Service.Services
{
    public interface ITokenService
    {
        Task<TokenResponse> GetTokenResponseAsync(User user);
        Task<string> GetAccessToken(User user);
        ClaimsPrincipal GetPrincipalFromExpiredAccessToken(string token);
    }

    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
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
                ExpiredAt = DateTime.UtcNow.AddMinutes(ExpiredAccessTime()),
                RefreshToken = await GetRefreshToken(user),
                TokenType = "Bearer "
            };
        }

        public async Task<string> GetRefreshToken(User user)
        {
            // Find the existing token for the user
            Token token = await _context.Tokens.FirstOrDefaultAsync(t => t.UserId == user.Id);

            // Check if the token is null or expired
            if (token == null || token.ExpiredTime < DateTime.UtcNow)
            {
                // Create a new token if it's null or expired
                Token newToken = new Token
                {
                    // Assume ExpiredRefreshTime() is a method that returns the number of days until expiration
                    ExpiredTime = DateTime.UtcNow.AddDays(ExpiredRefreshTime()),
                    UserId = user.Id,
                    RefreshToken = Guid.NewGuid().ToString(),
                    Id = Guid.NewGuid(), // Assuming Id is of type Guid
                };

                await _context.Tokens.AddAsync(newToken);
                await _context.SaveChangesAsync(); // Don't forget to save the changes to the database

                // Assign the new token to the token variable to return its RefreshToken
                token = newToken;
            }

            // Return the refresh token
            return token.RefreshToken;
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
                claims.Add(new Claim(permission.Type, permission.Value)); 
            }

            return AccessTokenGenerator(claims);
        }

        private string AccessTokenGenerator(List<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]!));


            var token = new JwtSecurityToken
                (
                    issuer: _config["JWT:ValidIssuer"],
                    audience: _config["JWT:ValidAudience"],
                    expires: DateTime.UtcNow.AddMinutes(ExpiredAccessTime()),
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

        public ClaimsPrincipal GetPrincipalFromExpiredAccessToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, 
                ValidateIssuer = false, 
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]!)), 
                ValidateLifetime = false 
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        private int ExpiredAccessTime() => int.TryParse(_config["JWT:AccessTokenValidityInMinutes"], out int accessTokenValidityInMinutes) ? accessTokenValidityInMinutes : 120;
        private int ExpiredRefreshTime() => int.TryParse(_config["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays) ? refreshTokenValidityInDays : 7;
    }
}

