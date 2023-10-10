
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BaseSource.Dto;
using BaseSource.Model;
using Microsoft.IdentityModel.Tokens;

namespace BaseSource.BackendAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepo;
        private readonly IRoleRepository _roleRepo;
        private readonly IRepository<AuthHistory> _historyRepo;

        public AuthService(IAuthRepository authRepo, IRepository<AuthHistory> historyRepo, IRoleRepository roleRepo)
        {
            _authRepo = authRepo;
            _roleRepo = roleRepo;
            _historyRepo = historyRepo;
        }
        public async Task<bool> UserExists(string username)
        {
            return await _authRepo.UserExists(username);
        }

        public async Task<Object> Login(LoginDto request)
        {
            var customer = await _authRepo.Login(request);
            
            if (customer == null || VerifyPasswordHash(request.Password, customer.Password))
                return new
                {
                    error = true,
                    message = "Tài khoản hoặc mật khẩu sai",
                };
            string token = await CreateToken(customer);
            AuthHistory history = new AuthHistory();
            history.Content = "Đã đăng nhập";
            history.CustomerId = customer.Id;
            _historyRepo.Create(history,customer.UserName);

            return new
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
            }; ;
        }

        public async Task<Object> Register(RegisterDto request)
        {

            var customer =  await _authRepo.Register(request,CreatePasswordHash(request.Password));
            string token = await CreateToken(customer);

            _roleRepo.CreateRole(customer.Id, customer.UserName);
            AuthHistory history = new AuthHistory();
            history.Content = "Đã đăng ký";
            history.CustomerId = customer.Id;
            _historyRepo.Create(history, customer.UserName);
            return new
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
        }
        
        bool VerifyPasswordHash(string password, string hashPasword)
        {
            return !BCrypt.Net.BCrypt.Verify(password, hashPasword);
        }

        string CreatePasswordHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password); ;
        }

        async Task<string> CreateToken(Customer customer)
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
            string jwt_token = await _authRepo.GenerateToken(customer.Id, jwt);
            return jwt_token;
        }

    }
}
