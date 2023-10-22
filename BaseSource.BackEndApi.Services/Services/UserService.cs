
using BaseSource.Dto;
using BaseSource.Model;
using Microsoft.AspNetCore.Identity;

namespace BaseSource.BackendAPI.Services
{
    public interface IUserService
    {
        Task<Response<bool>> UpdatePassword(ResetPasswordRequest request);
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

        public async Task<Response<bool>> UpdatePassword(ResetPasswordRequest request)
        {
            User isEmailExists = await _userManager.FindByEmailAsync(request.Email);

            if (isEmailExists is null)
                return ResponseHelper.CreateErrorResponse<bool>(409,"Email does not exists");

            ResetPassword reset = await _repository.GetById(request.IdResetPassword);

            if (reset.Enable == false || DateTime.Compare(reset.ExpiredTime,DateTime.Now)<0)
                return ResponseHelper.CreateErrorResponse<bool>(403, "Your request has been blocked");

            var removeOldPassword = await _userManager.RemovePasswordAsync(isEmailExists);

            if (!removeOldPassword.Succeeded)
                return ResponseHelper.CreateErrorResponse<bool>(500, "The server cannot process the request for an unknown reason");

            var addNewPassword = await _userManager.AddPasswordAsync(isEmailExists, request.Password);

            if (addNewPassword.Succeeded)
            {
                reset.IsVerify = true;
                reset.UpdatedAt = DateTime.UtcNow;
                reset.Enable = false;

                await _repository.Update(reset, isEmailExists.UserName);
                return ResponseHelper.CreateSuccessResponse<bool>(true);
            }
            else
            {
                return ResponseHelper.CreateErrorResponse<bool>(500, "The server cannot process the request for an unknown reason");
            }
        }
    }
}
