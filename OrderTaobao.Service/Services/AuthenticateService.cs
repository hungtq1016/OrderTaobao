
using Basesource.Constants;
using BaseSource.Builder;
using BaseSource.Dto;
using BaseSource.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace BaseSource.BackendAPI.Services
{
    public interface IAuthenticateService
    {
        Task<Response<TokenResponse>> Login(LoginRequest request);

        Task<Response<TokenResponse>> Register(RegisterRequest request);

        Task<Response<bool>> Logout(TokenRequest request);

        Task<Response<TokenResponse>> RefreshToken(TokenRequest request);

        Task<Response<PermissionResponse<User>>> GetPermission(TokenRequest request);
    }

    public class AuthenticateService : IAuthenticateService
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly IAuthHistoryService _historyService;
        private readonly UserManager<User> _userManager;

        public AuthenticateService(IConfiguration configuration, ITokenService tokenService,
            IAuthHistoryService historyService, UserManager<User> userManager)
        {
            _configuration = configuration;
            _tokenService = tokenService;
            _historyService = historyService;
            _userManager = userManager;
        }

        public async Task<Response<TokenResponse>> Login(LoginRequest request)
        {
            User? user = await _userManager.FindByNameAsync(request.UserName);
            if (user is null)
                return ResponseHelper.CreateErrorResponse<TokenResponse>(404, "Username or password is invalid");

            if (await _userManager.CheckPasswordAsync(user, request.Password))
            {
                IList<string> roles = await _userManager.GetRolesAsync(user);

                TokenResponse token = await _tokenService.CreateAccessToken(user, roles);

                await _historyService.CreateAuthHistory(user, UserConstant.Login);

                return ResponseHelper.CreateSuccessResponse<TokenResponse>(token);
            }

            return ResponseHelper.CreateErrorResponse<TokenResponse>(404, "Username or password is invalid");
        }

        public async Task<Response<TokenResponse>> Register(RegisterRequest request)
        {
         
            User? isUserNameExists = await _userManager.FindByNameAsync(request.UserName);
            User? isEmailExists = await _userManager.FindByEmailAsync(request.Email);

            if (isUserNameExists is not null)
                return ResponseHelper.CreateErrorResponse<TokenResponse>(409, "Username is already exists");

            if (isEmailExists is not null)
                return ResponseHelper.CreateErrorResponse<TokenResponse>(409, "Email is already exists");

            User user = new UserBuilder(_configuration)
                .WithId()
                .WithUserName(request.UserName)
                .WithEmail(request.Email)
                .WithFirstName(request.FirstName)
                .WithLastName(request.LastName)
                .WithPhone(request.Phone)
                .WithRefreshToken(TokenHelper.GenerateRefreshToken())
                .WithSecurityStamp()
                .WithEnable()
                .Build();

            var createResult = await _userManager.CreateAsync(user, request.Password);

            IList<string> roles = new List<string> { RolePermission.Customer };

            var addRoleResult = await _userManager.AddToRolesAsync(user, roles);
            
            if (!createResult.Succeeded || !addRoleResult.Succeeded)
                return ResponseHelper.CreateErrorResponse<TokenResponse>(500, "The server cannot process the request for an unknown reason");

            
            TokenResponse token = await _tokenService.CreateAccessToken(user, roles);

            await _historyService.CreateAuthHistory(user, UserConstant.Register);

            return ResponseHelper.CreateCreatedResponse<TokenResponse>(token);
        }

        public async Task<Response<bool>> Logout(TokenRequest request)
        {

            User? user = await GetUserByToken(request);

            if (user is null)
                return ResponseHelper.CreateErrorResponse<bool>(404, "User is not found");

            await _historyService.CreateAuthHistory(user, UserConstant.Logout);
            return ResponseHelper.CreateCreatedResponse(true);
        }

        public async Task<Response<TokenResponse>> RefreshToken(TokenRequest request)
        {
            bool isValidToken = _tokenService._isEmptyOrInvalid(request.AccessToken!);

            if (isValidToken)
            {
                TokenResponse tokenResponse =  await _tokenService.CreateNewAccessToken(request!);
                return ResponseHelper.CreateCreatedResponse(tokenResponse);
            }

            TokenResponse token =  new TokenResponse
            {
                AccessToken = request.AccessToken!,
                RefreshToken = request.RefreshToken!,
                ExpiredAt = DateTime.Now,
                TokenType = "Bearer"
            };

            return ResponseHelper.CreateSuccessResponse(token);
        }

        public async Task<Response<PermissionResponse<User>>> GetPermission(TokenRequest request)
        {
            User? user = await GetUserByToken(request);
            if (user is null)
                return ResponseHelper.CreateErrorResponse<PermissionResponse<User>> (404, "Token is not found");

            PermissionResponse<User> permission = new PermissionResponse<User>();

            var userRoles = await _userManager.GetRolesAsync(user);
            IList<string> roles = new List<string>{"Admin","Customer","Super Admin"};
            var match = userRoles.Intersect(roles);
            bool isAdmin = match.Any();

            permission.Data = user;
            permission.IsAuthen = true;
            permission.IsAdmin = isAdmin;

            return ResponseHelper.CreateSuccessResponse(permission);
            
        }

        private async Task<User> GetUserByToken(TokenRequest request)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken!);
            if (principal is null)
            {
                return null!;
            }
            string id = principal.Identity.Name;

            if (id is null)
                return null;

            return await _userManager.FindByIdAsync(id);
        }

    }
}
