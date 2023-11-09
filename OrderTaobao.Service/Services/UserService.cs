
using Azure.Core;
using BaseSource.BackendAPI.Services.Helpers;
using BaseSource.Builder;
using BaseSource.Dto;
using BaseSource.Dto.Request;
using BaseSource.Dto.Response;
using BaseSource.Helper;
using BaseSource.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace BaseSource.BackendAPI.Services
{
    public interface IUserService
    {
        Task<Response<PageResponse<List<UserResponse>>>> GetPagedData(PaginationRequest request, string route, bool enable);

        Task<Response<List<UserResponse>>> Get();

        Task<Response<UserDetailResponse>> GetById(string id);

        Task<Response<bool>> Add(UserRequest request);

        Task<Response<bool>> Update(string id, UserRequest request);

        Task<Response<bool>> UpdatePassword(ResetPasswordRequest request);

        Task<Response<bool>> Enable(string id,bool enable);

        Task<Response<List<string>>> MultipleEnable(MultipleRequest request, bool enable);

        Task<Response<bool>> Erase(string id);

        Task<Response<List<string>>> MultipleErase(MultipleRequest request);

        Task<ExcelResponse> Export();

        Task<Response<bool>> Import(IFormFile file);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<ResetPassword> _repository;
        private readonly IUriService _uriService;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;

        public UserService(UserManager<User> userManager, IRepository<ResetPassword> repository, IUriService uriService, IConfiguration configuration,IMemoryCache cache)
        {
            _userManager = userManager;
            _repository = repository;
            _uriService = uriService;
            _configuration = configuration;
            _cache = cache;
        }

        public async Task<Response<PageResponse<List<UserResponse>>>> GetPagedData(PaginationRequest request, string route,bool enable)
        {
            if (_userManager.Users is null)
                return ResponseHelper
                        .CreateErrorResponse<PageResponse<List<UserResponse>>>
                        (500, "The server cannot process the request for an unknown reason");

            var validFilter = new PaginationRequest(request.PageNumber, request.PageSize);

            var totalRecords = await _userManager.Users
                .Where(user => user.Enable == enable)
                .CountAsync();

            if (totalRecords == 0)
                return ResponseHelper.CreateErrorResponse<PageResponse<List<UserResponse>>>
                        (404, "No users found.");

            List<UserResponse> users = await _userManager.Users
                .Where(user => user.Enable == enable)
                .Select(user => new UserResponse
                {
                    Id = user.Id,
                    Email = user.Email!,
                    UserName = user.UserName!,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Phone = user.PhoneNumber!,
                    EmailConfirmed = user.EmailConfirmed,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                    Enable = user.Enable
                })
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize).ToListAsync();

            var pagedResponse = PaginationHelper.CreatePagedReponse<UserResponse>(users, validFilter, Convert.ToUInt16(totalRecords), _uriService, route);

            return ResponseHelper.CreateSuccessResponse(pagedResponse);
        }

        public async Task<Response<List<UserResponse>>> Get()
        {
            List<UserResponse>  users = await _userManager.Users
                .Select(user => new UserResponse
                {
                    Id = user.Id,
                    Email = user.Email!,
                    UserName = user.UserName!,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Phone = user.PhoneNumber!,
                    EmailConfirmed = user.EmailConfirmed,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                    Enable = user.Enable
                }).ToListAsync();
            return ResponseHelper.CreateSuccessResponse(users);
        }

        public async Task<Response<UserDetailResponse>> GetById(string id)
        {
            User? user = await _userManager.Users
                .Where(user => user.Id == id)
                .Where(user => user.Enable)
                .Include(user => user.Orders)!
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
                    Email = user.Email!,
                    UserName = user.UserName!,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Phone = user.PhoneNumber!,
                    EmailConfirmed = user.EmailConfirmed,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                    Enable = user.Enable
                },
                Notifications = user.Notifications!,
                Orders = user.Orders!,
                Roles = await _userManager.GetRolesAsync(user)
            };

            return ResponseHelper.CreateSuccessResponse(userDetail);
        }

        public async Task<Response<bool>> Add(UserRequest request)
        {
            User? isUserNameExists = await _userManager.FindByNameAsync(request.UserName);
            User? isEmailExists = await _userManager.FindByEmailAsync(request.Email);

            if (isUserNameExists is not null)
                return ResponseHelper.CreateErrorResponse<bool>(409, "Username is already exists");

            if (isEmailExists is not null)
                return ResponseHelper.CreateErrorResponse<bool>(409, "Email is already exists");

            User user = new UserBuilder(_configuration)
                .WithId()
                .WithLastName(request.LastName)
                .WithFirstName(request.FirstName)
                .WithUserName(request.UserName)
                .WithEmail(request.Email)
                .WithPhone(request.Phone)
                .WithRefreshToken(TokenHelper.GenerateRefreshToken())
                .WithSecurityStamp()
                .WithEnable()
                .Build();

            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
                return ResponseHelper
                    .CreateErrorResponse<bool>(500, "The server cannot process the request for an unknown reason");

            return ResponseHelper.CreateCreatedResponse(true);

        }

        public async Task<Response<bool>> Update(string id, UserRequest request)
        {
            if (id != request.Id)
                return ResponseHelper.CreateErrorResponse<bool>(404, "Can not found user");

            User? user = await _userManager.FindByIdAsync(request.Id);

            if (user is null || !user.Enable)
                return ResponseHelper.CreateErrorResponse<bool>(404, "Can not found user");

            User entityBuilder = new EntityBuilder<User>().ForEntity(user)
                .WithProperty(user => user.UserName!, request.UserName)
                .WithProperty(user => user.Email!, request.Email)
                .WithProperty(user => user.FirstName, request.FirstName)
                .WithProperty(user => user.LastName, request.LastName)
                .WithProperty(user => user.PhoneNumber!, request.Phone)
                .Build();

            return await Update(entityBuilder);

        }

        public async Task<Response<bool>> Enable(string id, bool enable)
        {
            User? user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                return ResponseHelper.CreateErrorResponse<bool>(404, "Can not found user");
            }
            user.Enable = enable;

            return await Update(user);
        }

        public async Task<Response<List<string>>> MultipleEnable(MultipleRequest request, bool enable)
        {
            List<string> responseList = new List<string>();

            foreach (string id in request.Ids)
            {
                User? user = await _userManager.FindByIdAsync(id);
                if (user is null)
                {
                    responseList.Add($"{id} : Fail");
                }
                else
                {
                    user.Enable = enable;
                    responseList.Add($"{id} : Pass");
                    await _userManager.UpdateAsync(user);
                }
            }

            return ResponseHelper.CreateSuccessResponse(responseList);
        }


        public async Task<Response<bool>> Erase(string id)
        {
            User? user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return ResponseHelper.CreateErrorResponse<bool>(404, "Can not found user");

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                return ResponseHelper
                    .CreateErrorResponse<bool>(500, "The server cannot process the request for an unknown reason");

            return ResponseHelper.CreateCreatedResponse(true);
        }

        public async Task<Response<List<string>>> MultipleErase(MultipleRequest request)
        {
            List<string> responseList = new List<string>();

            foreach (string id in request.Ids)
            {
                User? user = await _userManager.FindByIdAsync(id);
                if (user is null)
                {
                    responseList.Add($"{id} : Fail");
                }
                else
                {
                    user.Enable = false;
                    responseList.Add($"{id} : Pass");
                    await _userManager.DeleteAsync(user);
                }
            }

            return ResponseHelper.CreateSuccessResponse(responseList);
        }


        public async Task<Response<bool>> UpdatePassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
                return ResponseHelper.CreateErrorResponse<bool>(409, "Email does not exist");

            ResetPassword? reset = await _repository.ReadByIdAsync(request.IdResetPassword);
            if (reset is null)
                return ResponseHelper.CreateErrorResponse<bool>(403, "Your request has been blocked");

            if (!reset.Enable || DateTime.Compare(reset.ExpiredTime, DateTime.Now) < 0)
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

                await _repository.UpdateAsync(reset, user.UserName!);
                return ResponseHelper.CreateSuccessResponse<bool>(true);
            }
            else
            {
                return ResponseHelper.CreateErrorResponse<bool>(500, "The server cannot process the request for an unknown reason");
            }
        }

        private async Task<Response<bool>> Update(User user)
        {
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return ResponseHelper
                    .CreateErrorResponse<bool>(500, "The server cannot process the request for an unknown reason");

            return ResponseHelper.CreateSuccessResponse(true);
        }

        public async Task<ExcelResponse> Export()
        {
            string reportname = typeof(User).Name!;

            var list = await Get();
            var count = list.Data!.Count;
            if (count > 0)
            {
                var exportbytes = FileHelper.ExportToExcel(list.Data, reportname);

                return new ExcelResponse
                {
                    File = exportbytes,
                    Extension = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    Name = reportname
                };
            }

            return null!;
        }

        public async Task<Response<bool>> Import(IFormFile file)
        {
            string folder = $"Excel/{typeof(User).Name!}";
            List<IFormFile> files = new List<IFormFile>();

            files.Add(file);

            var result = await FileHelper.WriteFile(files, folder, "xlsx");

            if (result is null)
            {
                return ResponseHelper.CreateNotFoundResponse<bool>(typeof(User).FullName!);
            }
            return ResponseHelper.CreateSuccessResponse(true);
        }
    }
}
