
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using BaseSource.Dto;
using BaseSource.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BaseSource.BackendAPI.Services
{
    public interface IAuthenticateService
    {
        Task<AuthenResponse> Register(RegisterRequest request, string role);
        Task<AuthenResponse> Login(LoginRequest request);
        Task<PermissionResponse> CheckOAuth(TokenRequest model);
        Task<TokenResponse> RefreshToken(TokenRequest model);
        Task<TokenResponse> CreateNewAccessToken(TokenRequest request);
        Task<UserResponse> GetUserByToken(TokenRequest request);
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
                TokenResponse token = ResponseToken(roles, user);


                return new AuthenResponse { Error = false, StatusCode = 200, Message = "Đăng nhập thành công", Token = token };
            }

            //If null return error
            return new AuthenResponse { Error = true, StatusCode = 500, Message = "Tài khoản hoặc mật khẩu không chính xác!", Data = null! };
        }

        public async Task<AuthenResponse> Register(RegisterRequest request, string role = UserRoles.Customer)
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
            var result = await _authenRepo.CreateUserAsync(newUser, request.Password, role);

            //Return any errors if have when create like (password incorrect, phone,....) 
            if (!result.Succeeded)
                return new AuthenResponse { Error = true, StatusCode = 500, Message = "Có lỗi xảy ra! Thử lại sao ít phút", Data = result.ToString() };

            //Get all roles and permission by user
            var roles = await _authenRepo.GetRolesByUser(newUser);

            //Generate token and add claims user info and role
            TokenResponse token = ResponseToken(roles, newUser);


            return new AuthenResponse { Error = false, StatusCode = 200, Message = "Đăng ký thành công", Token = token };
        }
        public async Task<PermissionResponse> CheckOAuth(TokenRequest model)
        {
            var principal = GetPrincipalFromExpiredToken(model.AccessToken);
            if (principal is null)
            {
                return new PermissionResponse
                {
                    Error = true,
                    StatusCode = 200,
                    Message = "Không tìm thấy",
                };
            }
            string id = principal!.Identity!.Name!;
            var expired = principal.FindFirst("Expired")!.Value;
     
            
            var user =await _authenRepo.ReadUserAsync(id);

            IList<string> roles = await _authenRepo.GetRolesByUser(user);

            if (user is null)
                return new PermissionResponse
                {
                    Error = true,
                    StatusCode = 200,
                    Message = "Không tìm thấy",  
                };
            
            if (DateTime.Compare(DateTime.Now, DateTime.Parse(expired)) > 0)
            {
                return new PermissionResponse
                {
                    Error = true,
                    StatusCode = 200,
                    Message = "Hết hạn đăng nhập",      
                };
            }
            return new PermissionResponse
            {
                Error = false,
                StatusCode = 200,
                Message = "Đã đăng nhập",
                Roles = roles,
            }; 

        }
        public async Task<UserResponse> GetUserByToken(TokenRequest request)
        {
            var principal = GetPrincipalFromExpiredToken(request.AccessToken);
            if (principal is null)
            {
                return new UserResponse
                {
                    Error = true,
                    StatusCode = 200,
                    Message = "Không tìm thấy",
                };
            }
            string id = principal!.Identity!.Name!;
            var user = await _authenRepo.ReadUserAsync(id);
            UserResponse res =  new UserResponse
            { 
                Error = false,
                StatusCode = 200,
                Message = "Hoàn thành"
            };
            res.User.Add(user);
            return res;
        }

        public async Task<TokenResponse> CreateNewAccessToken(TokenRequest request)
        {
            _ = int.TryParse(_configuration["JWT:AccessTokenValidityInMinutes"], out int accessTokenValidityInMinutes);

            var principal = GetPrincipalFromExpiredToken(request.AccessToken);
            string id = principal!.Identity!.Name!;

            var user = await _authenRepo.ReadUserAsync(id);

            if (user.RefreshToken != request.RefreshToken)
            {
                return null!;
            }
            //If to day is bigger than expire time that mean token expire
            if (DateTime.Compare(DateTime.Now,user.RefreshTokenExpiryTime)>0)
            {
                await RefreshToken(request);
            }

            var authClaims = new List<Claim>
            {
                    new Claim("Id",user.Id!),
                    new Claim(ClaimTypes.Name,user.Id!),
                    new Claim("Email",user.Email!),
                    new Claim("Expired",DateTime.Now.AddMinutes(accessTokenValidityInMinutes).ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var access = GenerateAccessToken(authClaims);

            return new TokenResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(access),
                RefreshToken = user.RefreshToken!,
                TokenType = "Bearer",
                ExpiredAt = user.RefreshTokenExpiryTime
            };


        }


        private TokenResponse ResponseToken(IList<string> roles, User user)
        {
            _ = int.TryParse(_configuration["JWT:AccessTokenValidityInMinutes"], out int accessTokenValidityInMinutes);

            //Add data of user into claim
            var authClaims = new List<Claim>
            {
                    new Claim("Id",user.Id!),
                    new Claim(ClaimTypes.Name,user.Id!),
                    new Claim("Email",user.Email!),
                    new Claim("Expired",DateTime.Now.AddMinutes(accessTokenValidityInMinutes).ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            //Then add roles
            foreach (var role in roles)
            {
                authClaims.Add(new Claim("Role", role));
            }
            //Transform claim to token
            var token = GenerateAccessToken(authClaims);

            return new TokenResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = user.RefreshToken!,
                TokenType = "Bearer",
                ExpiredAt = user.RefreshTokenExpiryTime
            };
        }

        private JwtSecurityToken GenerateAccessToken(List<Claim> authClaims)
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

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
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

        public async Task<TokenResponse> RefreshToken(TokenRequest model)
        {
            var principal = GetPrincipalFromExpiredToken(model.AccessToken);
            string id = principal!.Identity!.Name!;

            if (principal == null || id==null)
            {
                return null!;
            }

            var user = await _authenRepo.ReadUserAsync(id!);
            if (user == null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return null!;
            }

            var newAccessToken = GenerateAccessToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _authenRepo.UpdateUserAsync(user);

            return new TokenResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken,
                ExpiredAt = DateTime.Now,
                TokenType = "Beared"
            };
        }

    }
}
