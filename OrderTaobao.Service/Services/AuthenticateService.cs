
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BaseSource.Dto;
using BaseSource.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BaseSource.BackendAPI.Services
{
    public interface IAuthenticateService
    {
        Task<AuthenResponse> Register(RegisterRequest request,string role);
        Task<AuthenResponse> Login(LoginRequest request);
    }

    public class AuthenticateService : IAuthenticateService
    {
        IAuthenticateRepository _authenRepo;
        IConfiguration _configuration;
        public AuthenticateService(IAuthenticateRepository authenRepo, IConfiguration configuration)
        {
            _authenRepo = authenRepo;
            _configuration = configuration;
        }
        public async Task<AuthenResponse> Login(LoginRequest request)
        {
            //Check if username exists
            var user = await _authenRepo.UserExists(request.UserName);

            if (user != null && await _authenRepo.IsPasswordValid(user, request.Password))
            {
                //Get all roles and permission by user
                var roles = await _authenRepo.GetRolesByUser(user);

                //Generate token and add claims user info and role
                TokenResponse token = GetToken(roles, user);


                return new AuthenResponse { Error = false, StatusCode = 200, Message = "Đăng nhập thành công", Token= token };
            }

            //If null return error
            return new AuthenResponse { Error = true, StatusCode = 500, Message = "Tài khoản hoặc mật khẩu không chính xác!",Data=null! };
        }

        public async Task<AuthenResponse> Register(RegisterRequest request,string role =UserRoles.Customer)
        {
            // Check if email or username exists
            var user = await _authenRepo.UserExists(request.UserName);
            var email = await _authenRepo.EmailExists(request.Email);
            if (user != null)
                return new AuthenResponse { Error = true, StatusCode = 500, Message = "Tài khoản đã tồn tại!", Data = null! };
            if (email != null)
                return new AuthenResponse { Error = true, StatusCode = 500, Message = "Email đã tồn tại!", Data = null! };
            var refreshToken = GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

     
            //If all pass then create new user
            User newUser = new()
            {
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays)
            };
            var result = await _authenRepo.CreateUserAsync(newUser,request.Password, role);

            //Return any errors if have when create like (password incorrect, phone,....) 
            if (!result.Succeeded)
                return new AuthenResponse { Error = true, StatusCode = 500, Message = "Có lỗi xảy ra! Thử lại sao ít phút", Data = result.ToString() };
            
            //Get all roles and permission by user
            var roles = await _authenRepo.GetRolesByUser(newUser);

            //Generate token and add claims user info and role
            TokenResponse token = GetToken(roles, newUser);


            return new AuthenResponse { Error = false, StatusCode = 200, Message = "Đăng ký thành công", Token = token };
        }

        private TokenResponse GetToken(IList<string> roles,User user)
        {
            //Add data of user into claim
            var authClaims = new List<Claim>
            {
                    new Claim(ClaimTypes.Name,user.UserName!),
                    new Claim(ClaimTypes.Email,user.Email!),
                    new Claim(ClaimTypes.NameIdentifier,user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            //Then add roles
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            //Transform claim to token
            var token = GenerateToken(authClaims);

            return new TokenResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = user.RefreshToken!,
                TokenType = "Bearer",
                ExpiredAt = user.RefreshTokenExpiryTime
            };
        }

        private JwtSecurityToken GenerateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));
            _ = int.TryParse(_configuration["JWT:AccessTokenValidityInMinutes"], out int accessTokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(accessTokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
