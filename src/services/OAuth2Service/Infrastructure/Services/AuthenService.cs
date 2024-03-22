using OAuth2Service.Authen;
using OAuth2Service.Models;

namespace OAuth2Service.Services
{
    public interface IAuthenService
    {
        Task<Response<TokenResponse>> LoginAsync(LoginRequest request);
        Task<Response<TokenResponse>> RegisterAsync(RegisterRequest request);
        Task<Response<bool>> SendResetPasswordAsync(ResetPasswordRequest request);
        Task<Response<bool>> SendOTPAsync(string email);
        Task<Response<bool>> ReceiveOTPAsync(OTPRequest request);
        Task<Response<TokenResponse>> GenerateAccessToken(Guid userId);
        Task<Response<TokenResponse>> RefreshAccessToken(TokenRequest request);
    }
    public class AuthenService : IAuthenService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<OTP> _otpRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IRabbitMQService _rabbitMQService;

        public AuthenService(ITokenService tokenService, IRepository<User> userRepository, IMapper mapper, IRabbitMQService rabbitMQService, IRepository<OTP> otpRepository)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
            _mapper = mapper;
            _rabbitMQService = rabbitMQService;
            _otpRepository = otpRepository;
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
            await _userRepository.AddAsync(response);

            return ResponseHelper.CreateCreatedResponse(await _tokenService.GetTokenResponseAsync(response));
        }

        public async Task<Response<TokenResponse>> GenerateAccessToken(Guid userId)
        {
            User user = await _userRepository.FindByIdAsync(userId);

            if (user is null)
            {
                return ResponseHelper.CreateErrorResponse<TokenResponse>(409, "User not found");
            }

            return ResponseHelper.CreateSuccessResponse(await _tokenService.GetTokenResponseAsync(user));
        }

        public async Task<Response<TokenResponse>> RefreshAccessToken(TokenRequest request)
        {
            ClaimsPrincipal? principal = _tokenService.GetPrincipalFromExpiredAccessToken(request.AccessToken);

            Guid userId = Guid.Parse(principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            return await GenerateAccessToken(userId);
        }

        public async Task<Response<bool>> SendResetPasswordAsync(ResetPasswordRequest request)
        {
            User userEmail = await FindByEmailAsync(request.Email);

            if (userEmail is null)
            {
                return ResponseHelper.CreateNotFoundResponse<bool>("Email not found!");
            }

            _rabbitMQService.SendMessage(request);

            return ResponseHelper.CreateSuccessResponse(true);
        }

        public async Task<Response<bool>> SendOTPAsync(string email)
        {
            User userEmail = await FindByEmailAsync(email);

            if (userEmail is null)
            {
                return ResponseHelper.CreateNotFoundResponse<bool>("Email not found!");
            }

            int code = GenerateOTP();

            OTP otp = new OTP
            {
                Email = email,
                Code = code,
                ExpiredTime = DateTime.UtcNow.AddMinutes(5)
            };

            await _otpRepository.AddAsync(otp);

            _rabbitMQService.SendMessage(otp);

            return ResponseHelper.CreateSuccessResponse(true);
        }

        public async Task<Response<bool>> ReceiveOTPAsync(OTPRequest request)
        {
            OTP exist = await _otpRepository.FindOneAsync(conditions: new Expression<Func<OTP, bool>>[]
                                                        {
                                                           otp => otp.Email == request.Email 
                                                           && otp.Code == request.OTP
                                                           && otp.ExpiredTime > DateTime.UtcNow
                                                        });
            if (exist is null)
            {
                return ResponseHelper.CreateNotFoundResponse<bool>("Expired time");
            }
            
            return ResponseHelper.CreateSuccessResponse(true);
        }

        private async Task<User> FindByEmailAsync(string email)
        {
            return await _userRepository
                        .FindOneAsync(conditions: new Expression<Func<User, bool>>[]
                        {
                           user => user.Email == email
                        });
        }

        private int GenerateOTP()
        {
            // Generate a random 6-digit OTP
            Random random = new Random();
            int otp = random.Next(100000, 999999);
            return otp;
        }
    }
}
