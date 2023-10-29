using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseSource.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthorizeController : ControllerBase
    {
        private readonly IAuthenticateService _authenService;
        private readonly ITokenService _tokenService;
        public AuthorizeController(IAuthenticateService authenService, ITokenService tokenService)
        {
            _authenService = authenService;
            _tokenService = tokenService;
        }

        // POST: api/<AuthorizeController>
        [HttpPost]
        [Route("permission")]
        public async Task<IActionResult> UserPermission(TokenRequest request)
        {
            if (request is null)
            {
                return Ok();
            }
            List<string> roles = new List<string> { "Admin", "Super Admin", "Manager" };

            var user = await _authenService.GetUserByToken(request);
            var user_roles = await _authenService.GetRolesByUser(user);
            var admin = _authenService.IsPermission(user_roles, roles);
            var tokenValidate = _tokenService._isEmptyOrInvalid(request.AccessToken!);
            bool isPermission = !tokenValidate;
            if (!isPermission)
            {
                return Ok(new PermissionResponse<UserResponse>
                    { Data = null,
                    Message = "Không Có Quyền",
                    IsAuthen = false,
                    AdminPermission = false,
                    Error = true,StatusCode=401});

            }
            var result = new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                UserName = user.UserName
            };

            return Ok(new PermissionResponse<UserResponse> { Data = result, Message = "Thành Công",IsAuthen = true,
                AdminPermission = admin,
            });
        }

    }
}
