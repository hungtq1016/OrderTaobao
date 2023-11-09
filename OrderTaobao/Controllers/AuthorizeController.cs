using BaseSource.BackendAPI.Authorization;
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
        public async Task<IActionResult> GetUser(TokenRequest request)
        {
            return await PerformAction(request, _authenService.GetPermission);
        }

        [HttpGet]
        [Route("authen")]
        public IActionResult IsAuthen()
        {
            return Ok();
        }

        [HttpGet]
        [Route("admin-view")]
        public IActionResult IsViewAdminPage()
        {
            return Ok();
        }
    }
}
