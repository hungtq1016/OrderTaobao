
using Microsoft.Extensions.Configuration;

namespace BaseSource.BackendAPI.Services
{
    public interface IAuthorizationService
    {

    }
    public class AuthorizationService : IAuthorizationService
    {
        IAuthenticateRepository _authenRepo;
        IConfiguration _configuration;
        public AuthorizationService(IAuthenticateRepository authenRepo, IConfiguration configuration)
        {
            _authenRepo = authenRepo;
            _configuration = configuration;
        }
    }
}
