using AutoMapper;
using Infrastructure.EFCore.DTOs;
using Infrastructure.EFCore.Helpers;
using Infrastructure.EFCore.Repository;
using OAuth2Service.DTOs;
using OAuth2Service.Models;
using System.Linq.Expressions;
using BC = BCrypt.Net.BCrypt;

namespace OAuth2Service.Services
{
    public interface IAuthenService
    {
        Task<Response<TokenResponse>> LoginAsync(LoginRequest request);
        Task<Response<TokenResponse>> RegisterAsync(RegisterRequest request);
        Task<Response<bool>> ResetPasswordAsync(ResetPasswordRequest request);
       /* Task<Response<TokenResponse>> RefreshToken(TokenRequest request);*/
    }
    public class AuthenService : IAuthenService
    {
        private readonly IRepository<User> _repository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthenService(ITokenService tokenService, IRepository<User> userRepo, IMapper mapper)
        {
            _tokenService = tokenService;
            _repository = userRepo;
            _mapper = mapper;
        }

        public async Task<Response<TokenResponse>> LoginAsync(LoginRequest request)
        {
            User user = await FindByEmailAsync(request.Email);

            if (user is null || !BC.Verify(request.Password, user.Password))
            {
                return ResponseHelper.CreateNotFoundResponse<TokenResponse>("Email or Password is invalid!");
            }

            return ResponseHelper.CreateSuccessResponse(await _tokenService.GetTokenResponseAsync(user));
        }

        public async Task<Response<TokenResponse>> RegisterAsync(RegisterRequest request)
        {
            User userEmail = await FindByEmailAsync(request.Email);

            if (userEmail is not null)
            {
                return ResponseHelper.CreateErrorResponse<TokenResponse>(409, "Email is already exists");
            }

            var response = _mapper.Map<User>(request);
            response.Password = BC.HashPassword(response.Password);
            await _repository.AddAsync(response);

            return ResponseHelper.CreateCreatedResponse(await _tokenService.GetTokenResponseAsync(response));
        }

        public async Task<Response<bool>> ResetPasswordAsync(ResetPasswordRequest request)
        {
            User user = await FindByEmailAsync(request.Email);

            if (user is null)
                return ResponseHelper.CreateNotFoundResponse<bool>("Email wrong or time expired");

            user.Password = BC.HashPassword(request.Password);

            await _repository.EditAsync(user);

            return ResponseHelper.CreateSuccessResponse(true);
        }

        private async Task<User> FindByEmailAsync(string email)
        {
            return await _repository
                        .FindOneAsync(conditions: new Expression<Func<User, bool>>[]
                        {
                                user => user.Email == email
                            });
        }
    }
}
