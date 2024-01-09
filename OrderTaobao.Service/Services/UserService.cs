
using AutoMapper;
using Basesource.Constants;
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
        Task<Response<PageResponse<List<UserResponse>>>> GetPagedData(PaginationRequest request, string route);

        Task<Response<List<UserResponse>>> Get();

        Task<Response<UserResponse>> GetById(string id);

        Task<Response<UserResponse>> Add(UserRequest request);

        Task<Response<UserResponse>> Update(string id, UserRequest request);

        Task<Response<List<String>>> MultipleUpdate(MultipleRequest request);

        Task<Response<bool>> UpdatePassword(ResetPasswordRequest request);

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
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, IRepository<ResetPassword> repository, IUriService uriService, IConfiguration configuration,IMemoryCache cache, IMapper mapper)
        {
            _userManager = userManager;
            _repository = repository;
            _uriService = uriService;
            _configuration = configuration;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<Response<PageResponse<List<UserResponse>>>> GetPagedData(PaginationRequest request, string route)
        {
            if (_userManager.Users is null)
                return ResponseHelper
                        .CreateErrorResponse<PageResponse<List<UserResponse>>>
                        (500, "The server cannot process the request for an unknown reason");

            var validFilter = new PaginationRequest(request.PageNumber, request.PageSize);

            var totalRecords = await _userManager.Users
                .CountAsync();

            if (totalRecords == 0)
                return ResponseHelper.CreateErrorResponse<PageResponse<List<UserResponse>>>
                        (404, "No users found.");

            var query = _userManager.Users.AsQueryable();

            if (request.Status == StatusEnum.Disable || request.Status == StatusEnum.Enable)
            {
                query = query.Where(user => user.Enable == (request.Status == StatusEnum.Enable));
            }

            var lists = await query
                .Select(user => _mapper.Map<UserResponse>(user))
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();

            var pagedResponse = PaginationHelper.CreatePagedReponse(lists, validFilter, Convert.ToUInt16(totalRecords), _uriService, route);

            return ResponseHelper.CreateSuccessResponse(pagedResponse);
        }

        public async Task<Response<List<UserResponse>>> Get()
        {
            if (_cache.TryGetValue("users", out List<UserResponse>? users))
            {
                users = await _userManager.Users
                .Select(user => _mapper.Map<UserResponse>(user))
                .ToListAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                                   .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                                   .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                                   .SetPriority(CacheItemPriority.Normal)
                                   .SetSize(1024);
            }

            return ResponseHelper.CreateSuccessResponse(users)!;
        }

        public async Task<Response<UserResponse>> GetById(string id)
        {
            User? user = await _userManager.Users
                .Where(user => user.Id == id)
                .Where(user => user.Enable)
                .Include(user => user.Orders)!
                    .ThenInclude(order => order.Details)
                .Include(user => user.Notifications)
                .FirstOrDefaultAsync();

            if (user is null)
                return ResponseHelper.CreateErrorResponse<UserResponse>(404, "No user found.");

            var result = _mapper.Map<UserResponse>(user);

            return ResponseHelper.CreateSuccessResponse(result);
        }

        public async Task<Response<UserResponse>> Add(UserRequest request)
        {
            User? isUserNameExists = await _userManager.FindByNameAsync(request.UserName);
            User? isEmailExists = await _userManager.FindByEmailAsync(request.Email);

            if (isUserNameExists is not null)
                return ResponseHelper.CreateErrorResponse<UserResponse>(409, "Username is already exists");

            if (isEmailExists is not null)
                return ResponseHelper.CreateErrorResponse<UserResponse>(409, "Email is already exists");

            User user = new UserBuilder(_configuration)
                .WithId()
                .WithRefreshToken(TokenHelper.GenerateRefreshToken())
                .WithSecurityStamp()
                .WithEnable()
                .Build();

            _mapper.Map(request, user);

            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
                return ResponseHelper
                    .CreateErrorResponse<UserResponse>(500, "The server cannot process the request for an unknown reason");

            UserResponse response = _mapper.Map<UserResponse>(user);

            return ResponseHelper.CreateCreatedResponse(response);

        }

        public async Task<Response<UserResponse>> Update(string id, UserRequest request)
        {
            User? user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return ResponseHelper.CreateErrorResponse<UserResponse>(404, "Can not found user");

            var update = _mapper.Map<User>(request);

            return await Update(update);

        }

        public async Task<Response<List<string>>> MultipleUpdate(MultipleRequest request)
        {
            List<string> response = new List<string>();

            Parallel.ForEach(request.Ids, async id =>
            {
                User? user = await _userManager.FindByIdAsync(id);
                if (user is null)
                {
                    response.Add($"{id} : Fail");
                }
                else
                {
                    user.Enable = request.Enable;
                    response.Add($"{id} : Pass");
                    await _userManager.UpdateAsync(user);
                }
            });

            return ResponseHelper.CreateSuccessResponse(response);
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

                await _repository.UpdateAsync(reset);
                return ResponseHelper.CreateSuccessResponse(true);
            }
            else
            {
                return ResponseHelper.CreateErrorResponse<bool>(500, "The server cannot process the request for an unknown reason");
            }
        }

        private async Task<Response<UserResponse>> Update(User user)
        {
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return ResponseHelper
                    .CreateErrorResponse<UserResponse>(500, "The server cannot process the request for an unknown reason");

            UserResponse response = _mapper.Map<UserResponse>(user);

            return ResponseHelper.CreateSuccessResponse(response);
        }

        public async Task<ExcelResponse> Export()
        {
            string reportname = typeof(User).FullName!;

            var list = await Get();

            if (list.Data!.Count > 0)
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
            string folder = $"Excel\\{typeof(User).FullName!}";
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
