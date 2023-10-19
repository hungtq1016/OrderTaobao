

using BaseSource.Dto;
using BaseSource.Model;
using Microsoft.Extensions.Configuration;

namespace BaseSource.BackendAPI.Services
{
    public interface IAuthenticateService
    {
        Task<Response<TokenResponse>> Login(LoginRequest request);
        Task<Response<TokenResponse>> Register(RegisterRequest request, string role);
        Task<Response<TokenResponse>> RefreshToken(TokenRequest request);
        Task<User> GetUserByToken(TokenRequest request);
        Task<IList<string>> GetRolesByUser(User user);
        bool IsPermission(IList<string> userRoles, IList<string> roles);
    }

    public class AuthenticateService : IAuthenticateService
    {
        private readonly IAuthenticateRepository _authenRepo;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly IAuthHistoryService _historyService;
        public AuthenticateService(IAuthenticateRepository authenRepo, IConfiguration configuration, ITokenService tokenService, IAuthHistoryService historyService)
        {
            _authenRepo = authenRepo;
            _configuration = configuration;
            _tokenService = tokenService;
            _historyService = historyService;
        }
        public async Task<Response<TokenResponse>> Login(LoginRequest request)
        {
            //Check if username exists
            var user = await _authenRepo.UserExists(request.UserName);

            if (user != null && await _authenRepo.IsPasswordValid(user, request.Password))
            {
                //Get all roles and permission by user
                var roles = await _authenRepo.GetRolesByUser(user);

                //Generate token and add claims user info and role
                TokenResponse token = _tokenService.CreateAccessToken(user, roles);
                await _historyService.CreateAuthHistory(user, "Đã đăng nhập");

                return new Response<TokenResponse>{Data = token, Message= "Đăng Nhập Thành Công" }; ;
            }

            //If null return error
            return new Response<TokenResponse> { Error = true, StatusCode = 500, Message = "Có Lỗi Xảy Ra", Data = null! };
        }

        public async Task<Response<TokenResponse>> Register(RegisterRequest request, string role = RolePermission.Customer)
        {
            // Check if email or username exists
            var user = await _authenRepo.UserExists(request.UserName);
            var email = await _authenRepo.EmailExists(request.Email);
            if (user != null)
                return new Response<TokenResponse> { Error = true, StatusCode = 500, Message = "Tài khoản đã tồn tại!", Data = null! };
            if (email != null)
                return new Response<TokenResponse> { Error = true, StatusCode = 500, Message = "Email đã tồn tại!", Data = null! };
            var refreshToken = _tokenService.GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);


            //If all pass then create new user
            User newUser = new()
            {
                Id = Guid.NewGuid().ToString()  ,
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                FirstName = request.FirstName,
                PhoneNumber = request.Phone,
                LastName = request.LastName,
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays)
            };
            var result = await _authenRepo.CreateUserAsync(newUser, request.Password, role);

            //Return any errors if have when create like (password incorrect, phone,....) 
            if (!result.Succeeded)
                return new Response<TokenResponse> { Error = true, StatusCode = 500, Message = "Có Lỗi Xảy Ra", Data = null! };

            //Get all roles and permission by user
            var roles = await _authenRepo.GetRolesByUser(newUser);

            //Generate token and add claims user info and role
            TokenResponse token = _tokenService.CreateAccessToken(newUser, roles);
            await _historyService.CreateAuthHistory(newUser, "Đã đăng ký");
            return new Response<TokenResponse> { Message = "Đăng ký thành công", Data = token };
        }
        public async Task<Response<TokenResponse>> RefreshToken(TokenRequest request)
        {
            if (_tokenService._isEmptyOrInvalid(request.AccessToken!))
            {
                var result =  await _tokenService.CreateNewAccessToken(request!);
                return new Response<TokenResponse> { Message = "Thành công", Data = result };
            }

            TokenResponse token =  new TokenResponse
            {
                AccessToken = request.AccessToken!,
                RefreshToken = request.RefreshToken!,
                ExpiredAt = DateTime.Now,
                TokenType = "Bearer"
            };
            return new Response<TokenResponse> { Message = "Còn hạn sử dụng", Data = token };

        }
        public async Task<User> GetUserByToken(TokenRequest request)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken!);
            if (principal is null)
            {
                return null!;
            }
            string id = principal!.Identity!.Name!;
            var user = await _authenRepo.ReadUserAsync(id);
            
            return user;
        }

        public async Task<IList<string>> GetRolesByUser(User user)
        {
            return await _authenRepo.GetRolesByUser(user);
        }
        public bool IsPermission(IList<string> userRoles, IList<string> roles)
        {
            for (int i = 0; i < userRoles.Count; i++)
            {
                if (roles.Any(x => x.ToLower() == userRoles[i].ToLower()))
                {
                    return true;
                }
                
            }
            return false;
        }
    }
}
