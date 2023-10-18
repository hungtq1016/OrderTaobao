using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseSource.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [Route("admin-permission")]
        public async Task<IActionResult> AdminPermission(TokenRequest request)
        {
            if (request is null)
            {
                return Ok();
            }
            List<string> roles = new List<string> { "Admin", "SuperAdmin", "Manager" };

            var user = await _authenService.GetUserByToken(request);
            var user_roles = await _authenService.GetRolesByUser(user);

            var permission = _authenService.IsPermission(user_roles, roles);
            var tokenValidate = _tokenService._isEmptyOrInvalid(request.AccessToken!);
            bool isPermission = permission && !tokenValidate;
            if (!isPermission)
            {
                return Ok(new
                {
                    User = false,
                    IsPermission = false,
                    Error = true,
                    Message = "Không có quyền"
                });

            }
            return Ok(new
            {
                User = new
                {
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    user.UserName,
                },
                IsPermission = true,
                Error = false,
                Message = "Thành Công"
            });
        }

        [HttpPost]
        [Route("user-permission")]
        public async Task<IActionResult> UserPermission(TokenRequest request)
        {
            if (request is null)
            {
                return Ok();
            }
            List<string> roles = new List<string> { "Admin", "SuperAdmim", "Staff", "Manager", "Collaborator", "Visitor", "Customer" };

            var user = await _authenService.GetUserByToken(request);
            var user_roles = await _authenService.GetRolesByUser(user);

            var permission = _authenService.IsPermission(user_roles, roles);
            var tokenValidate = _tokenService._isEmptyOrInvalid(request.AccessToken!);
            bool isPermission = permission && !tokenValidate;
            if (!isPermission)
            {
                return Ok(new
                {
                    User = false,
                    IsPermission = false,
                    Error = true,
                    Message = "Không có quyền"
                });

            }
            return Ok(new
            {
                User = new
                {
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    user.UserName,
                },
                IsPermission = true,
                Error = false,
                Message = "Thành Công"
            });
        }

    }
}
