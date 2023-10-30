using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseSource.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorizeController : StatusController
    {
        private readonly IAuthenticateService _authenService;
        public AuthorizeController(IAuthenticateService authenService)
        {
            _authenService = authenService;
        }

        // POST: api/<AuthorizeController>
        [HttpPost]
        [Route("user-info")]
        [Authorize]
        public async Task<IActionResult> GetUser(TokenRequest request)
        {
            return await PerformAction(request, _authenService.GetPermission);
        }

        [HttpGet]
        [Route("authen")]
        [Authorize]
        public IActionResult IsAuthen()
        {
            return Ok();
        }

        [HttpGet]
        [Route("admin-view")]
        [Authorize(Policy = "AdminView")]
        public IActionResult IsViewAdminPage()
        {
            return Ok();
        }
    }
}
