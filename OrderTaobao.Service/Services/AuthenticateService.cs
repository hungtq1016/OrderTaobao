
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Azure.Core;
using BaseSource.Dto;
using BaseSource.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BaseSource.BackendAPI.Services
{
    public interface IAuthenticateService
    {
        Task<ResponseDto> Register(RegisterDto request,string role);
        Task<ResponseDto> Login(LoginDto request);
        JwtSecurityToken GetToken(List<Claim> authClaims);
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
        public async Task<ResponseDto> Login(LoginDto request)
        {
            var user = await _authenRepo.UserExists(request.UserName);
            if (user != null && await _authenRepo.IsPasswordValid(user, request.Password))
            {
                var roles = await _authenRepo.GetRolesByUser(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var role in roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                var token = GetToken(authClaims);

                return new ResponseDto { Error = false, StatusCode = 200, Message = "Đăng nhập thành công",Data= new JwtSecurityTokenHandler().WriteToken(token) };
            }
            return new ResponseDto { Error = true, StatusCode = 500, Message = "Tài khoản hoặc mật khẩu không chính xác!",Data=null! };
        }

        public async Task<ResponseDto> Register(RegisterDto request,string role =UserRoles.Customer)
        {
            var user = await _authenRepo.UserExists(request.UserName);
            var email = await _authenRepo.EmailExists(request.Email);
            if (user != null)
                return new ResponseDto { Error = true, StatusCode = 500, Message = "Tài khoản đã tồn tại!", Data = null! };
            if (email != null)
                return new ResponseDto { Error = true, StatusCode = 500, Message = "Email đã tồn tại!", Data = null! };

            IdentityUser newUser = new()
            {
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.UserName
            };
            var result = await _authenRepo.Register(newUser,request.Password, role);
            if (result == null)
                return new ResponseDto { Error = true, StatusCode = 500, Message = "Có lỗi xảy ra! Thử lại sao ít phút", Data = null };
            return new ResponseDto { Error = false, StatusCode = 200, Message = "Đăng ký thành công", Data = result.ToString() };
        }

        public JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
