

using Basesource.Constants;
using BaseSource.Builder;
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
            User isUserNameExists = await _authenRepo.UserExists(request.UserName);
            bool isPasswordCorrect = await _authenRepo.IsPasswordValid(isUserNameExists, request.Password);

            if (isUserNameExists is not null && isPasswordCorrect)
            {
                IList<string> roles = await _authenRepo.GetRolesByUser(isUserNameExists);

                TokenResponse token = _tokenService.CreateAccessToken(isUserNameExists, roles);

                await _historyService.CreateAuthHistory(isUserNameExists, UserConstant.Login);

                return ResponseHelper.CreateSuccessResponse<TokenResponse>(token);
            }

            return ResponseHelper.CreateErrorResponse<TokenResponse>(404, "Username or password is invalid");
        }

        public async Task<Response<TokenResponse>> Register(RegisterRequest request, string role = RolePermission.Customer)
        {
         
            User isUserNameExists = await _authenRepo.UserExists(request.UserName);
            User isEmailExists = await _authenRepo.EmailExists(request.Email);

            if (isUserNameExists is not null)
                return ResponseHelper.CreateErrorResponse<TokenResponse>(409, "Username is already exists");

            if (isEmailExists is not null)
                return ResponseHelper.CreateErrorResponse<TokenResponse>(409, "Email is already exists");

            string refreshToken = _tokenService.GenerateRefreshToken();

            User user = new UserBuilder(_configuration)
                .WithId()
                .WithUserName(request.UserName)
                .WithEmail(request.Email)
                .WithFirstName(request.FirstName)
                .WithLastName(request.LastName)
                .WithPhone(request.Phone)
                .WithRefreshToken(refreshToken)
                .WithSecurityStamp()
                .Build();

            var result = await _authenRepo.CreateUserAsync(user, request.Password, role);

            if (!result.Succeeded)
                return ResponseHelper.CreateErrorResponse<TokenResponse>(500, "The server cannot process the request for an unknown reason");

            var roles = await _authenRepo.GetRolesByUser(user);

            TokenResponse token = _tokenService.CreateAccessToken(user, roles);

            await _historyService.CreateAuthHistory(user, UserConstant.Register);

            return ResponseHelper.CreateCreatedResponse<TokenResponse>(token);
        }

        public async Task<Response<TokenResponse>> RefreshToken(TokenRequest request)
        {
            bool isValidToken = _tokenService._isEmptyOrInvalid(request.AccessToken!);

            if (isValidToken)
            {
                TokenResponse tokenResponse =  await _tokenService.CreateNewAccessToken(request!);
                return ResponseHelper.CreateCreatedResponse<TokenResponse>(tokenResponse);
            }

            TokenResponse token =  new TokenResponse
            {
                AccessToken = request.AccessToken!,
                RefreshToken = request.RefreshToken!,
                ExpiredAt = DateTime.Now,
                TokenType = "Bearer"
            };

            return ResponseHelper.CreateSuccessResponse<TokenResponse>(token);

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
