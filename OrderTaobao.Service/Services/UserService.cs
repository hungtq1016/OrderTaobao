
using BaseSource.Dto;
using BaseSource.Model;
using Microsoft.AspNetCore.Identity;

namespace BaseSource.BackendAPI.Services
{
    public interface IUserService
    {
        Task<bool> UpdatePassword(ResetPasswordRequest request);
    }
    public class UserService :IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<ResetPassword> _repository;
        public UserService(UserManager<User> userManager, IRepository<ResetPassword> repository)
        {
            _userManager = userManager;
            _repository = repository;
        }

        public async Task CreateUser()
        {

        }

        public async Task<bool> UpdatePassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                return false;
            var result = await _userManager.RemovePasswordAsync(user);
            if (!result.Succeeded)
                return false;

            var updatePass = await _userManager.AddPasswordAsync(user,request.Password);
            if (updatePass.Succeeded)
            {
                ResetPassword reset = await _repository.GetById(request.IdResetPassword);
                reset.IsVerify = true;
                reset.UpdatedAt = DateTime.UtcNow;
                reset.Enable = false;
                await _repository.Update(reset, user.UserName);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
