
using BaseSource.Builder;
using BaseSource.Dto;
using BaseSource.Helper;
using BaseSource.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BaseSource.BackendAPI.Services
{
    public interface IUserService
    {
        Task<Response<PageResponse<List<UserResponse>>>> GetAllWithPagination(PaginationRequest request, string route);

        Task<Response<UserDetailResponse>> GetUserDetailById(string id);

        Task<Response<bool>> UpdatePassword(ResetPasswordRequest request);
    }
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<ResetPassword> _repository;
        private readonly IUriService _uriService;

        public UserService(UserManager<User> userManager, IRepository<ResetPassword> repository, IUriService uriService)
        {
            _userManager = userManager;
            _repository = repository;
            _uriService = uriService;
        }

        public async Task<Response<PageResponse<List<UserResponse>>>> GetAllWithPagination(PaginationRequest request, string route)
        {
            if (_userManager.Users is null)
            {
                return ResponseHelper
                    .CreateErrorResponse<PageResponse<List<UserResponse>>>
                    (500, "The server cannot process the request for an unknown reason");
            }

            var validFilter = new PaginationRequest(request.PageNumber, request.PageSize);

            var totalRecords = await _userManager.Users
                .Where(user => user.Enable)
                .CountAsync();

            if (totalRecords == 0)
            {
                return ResponseHelper.CreateErrorResponse<PageResponse<List<UserResponse>>>
                    (404, "No users found.");
            }

            List<UserResponse> users = await _userManager.Users
                .Where(user => user.Enable)
                .Select(user => new UserResponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Phone = user.PhoneNumber,
                    EmailConfirmed = user.EmailConfirmed,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                    Enable = user.Enable
                })
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize).ToListAsync();

            var pagedReponse = PaginationHelper.CreatePagedReponse<UserResponse>(users, validFilter, totalRecords, _uriService, route);

            return ResponseHelper.CreateSuccessResponse<PageResponse<List<UserResponse>>>(pagedReponse);
        }

        public async Task<Response<UserDetailResponse>> GetUserDetailById(string id)
        {
            var user = await _userManager.Users
                .Where(user => user.Id == id)
                .Where(user => user.Enable)
                .Include(user => user.Orders)
                    .ThenInclude(order => order.Details)
                .Include(user => user.Notifications)
                .FirstOrDefaultAsync();

            if (user is null)
                return ResponseHelper.CreateErrorResponse<UserDetailResponse>(404, "No user found.");

            var userDetail = new UserDetailResponse
            {
                User = new UserResponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Phone = user.PhoneNumber,
                    EmailConfirmed = user.EmailConfirmed,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                    Enable = user.Enable
                },
                Notifications = user.Notifications,
                Orders = user.Orders,
                Roles = await _userManager.GetRolesAsync(user)
            };

            return ResponseHelper.CreateSuccessResponse<UserDetailResponse>(userDetail);
        }

        public async Task<Response<bool>> UpdatePassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                return ResponseHelper.CreateErrorResponse<bool>(409, "Email does not exist");

            var reset = await _repository.GetById(request.IdResetPassword);

            if (!reset.Enable || reset.ExpiredTime < DateTime.Now)
                return ResponseHelper.CreateErrorResponse<bool>(403, "Your request has been blocked");

            var removeOldPasswordResult = await _userManager.RemovePasswordAsync(user);

            if (!removeOldPasswordResult.Succeeded)
                return ResponseHelper.CreateErrorResponse<bool>(500, "The server cannot process the request for an unknown reason");

            var addNewPasswordResult = await _userManager.AddPasswordAsync(user, request.Password);

            if (addNewPasswordResult.Succeeded)
            {
                reset.IsVerify = true;
                reset.UpdatedAt = DateTime.UtcNow;
                reset.Enable = false;

                await _repository.Update(reset, user.UserName!);
                return ResponseHelper.CreateSuccessResponse<bool>(true);
            }
            else
            {
                return ResponseHelper.CreateErrorResponse<bool>(500, "The server cannot process the request for an unknown reason");
            }
        }

    }
}
