
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;

namespace OrderTaobao.Services.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public static Customer customer = new Customer();
        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> UserExists(string username)
        {
            if (await _context.Customers.AnyAsync(c => c.UserName == username))
                return true;
            else
                return false;
        }

        public async Task<Object> Login(LoginDto request)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserName == request.UserName);
            var error = new
            {
                error = true,
                message = "Tài khoản hoặc mật khẩu sai",
            };
            if (customer == null)
                return error;

            if (VerifyPasswordHash(request.Password, customer.Password))
                return error;
            string token = CreateToken(customer);
            var response = new
            {
                data = new
                {
                    customer.FirstName,
                    customer.LastName,
                    customer.Email,
                    customer.Phone,
                    customer.UserName
                },
                error = false,
                message = "Đăng nhập thành công",
                token_code = token,
                token_type = "jwt"
            };
            return response;
        }

        public async Task<Object> Register(RegisterDto request)
        {
            customer.Id = Guid.NewGuid();
            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.UserName = request.UserName.ToLower();
            customer.Phone = request.Phone;
            customer.Email = request.Email;
            customer.RememberToken = Guid.NewGuid();
            customer.Enable = true;
            customer.Password = CreatePasswordHash(request.Password);

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            string token = CreateToken(customer);

            var response = new
            {
                data = new
                {
                    customer.FirstName,
                    customer.LastName,
                    customer.Email,
                    customer.Phone,
                    customer.UserName
                },
                error = false,
                message = "Đăng ký thành công",
                token_code = token,
                token_type = "jwt"
            };
            return response;
        }
        
        internal bool VerifyPasswordHash(string password, string hashPasword)
        {
            return !BCrypt.Net.BCrypt.Verify(password, hashPasword);
        }

        internal string CreatePasswordHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password); ;
        }

        internal string CreateToken(Customer customer)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, customer.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Secret from my code"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(7),
                    signingCredentials: credentials
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
