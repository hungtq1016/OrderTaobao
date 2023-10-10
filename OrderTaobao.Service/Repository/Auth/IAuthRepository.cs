using BaseSource.Dto;
using BaseSource.Model;

namespace BaseSource.BackendAPI.Services
{
    public interface IAuthRepository
    {
        Task<Customer> Register(RegisterDto request, string hashPassword);

        Task<Customer> Login(LoginDto request);

        Task<bool> UserExists(string username);

        Task<string> GenerateToken(Guid customerId,string token);

        void Save();

    
    }
}
