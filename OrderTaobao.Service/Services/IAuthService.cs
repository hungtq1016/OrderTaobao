using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseSource.Dto;

namespace BaseSource.BackendAPI.Services
{
    public interface IAuthService 
    {
        Task<bool> UserExists(string username);
        Task<Object> Login(LoginDto request);
        Task<Object> Register(RegisterDto request);

    }
}
