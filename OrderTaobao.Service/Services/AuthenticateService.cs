
using BaseSource.Dto;
using BaseSource.Model;
using Microsoft.Extensions.Configuration;

namespace BaseSource.BackendAPI.Services
{
    public interface IAuthenticateService
    {
        Task<AuthenResponse> Register(RegisterRequest request, string role);
        Task<AuthenResponse> Login(LoginRequest request);
        Task<TokenResponse> RefreshToken(TokenRequest request);
        Task<UserResponse> GetUserByToken(TokenRequest request);
    }

    public class AuthenticateService : IAuthenticateService
    {
        IAuthenticateRepository _authenRepo;
        IConfiguration _configuration;
        ITokenService _tokenService;
        public AuthenticateService(IAuthenticateRepository authenRepo, IConfiguration configuration, ITokenService tokenService)
        {
            _authenRepo = authenRepo;
            _configuration = configuration;
            _tokenService = tokenService;
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
                TokenResponse token = _tokenService.CreateAccessToken(user, roles);


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
            var refreshToken = _tokenService.GenerateRefreshToken();

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
            TokenResponse token = _tokenService.CreateAccessToken(newUser, roles);

            return new AuthenResponse { Error = false, StatusCode = 200, Message = "Đăng ký thành công", Token = token };
        }
        public async Task<TokenResponse> RefreshToken(TokenRequest request)
        {
            if (_tokenService._isEmptyOrInvalid(request.AccessToken!))
            {
                return  await _tokenService.CreateNewAccessToken(request!);
            }

            return new TokenResponse
            {
                AccessToken = request.AccessToken!,
                RefreshToken = request.RefreshToken!,
                ExpiredAt = DateTime.Now,
                TokenType = "Bearer"
            };

        }
        public async Task<UserResponse> GetUserByToken(TokenRequest request)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken!);
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

    }
}
