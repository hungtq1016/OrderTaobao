namespace OrderTaobao.Services.Auth
{
    public interface IAuthRepository
    {
        Task<Object> Register(RegisterDto request);

        Task<Object> Login(LoginDto request);

        Task<bool> UserExists(string username);
    }
}
