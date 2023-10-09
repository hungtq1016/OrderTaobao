using Microsoft.AspNetCore.Mvc;
using OrderTaobao.Services.Auth;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderTaobao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }
        // POST api/<AuthController>/Login
        [HttpPost("Login")]
        public async Task<ActionResult<Customer>> Login(LoginDto request)
        {
            var customer = await _repo.Login(request);
            return Ok(customer);
        }

        // POST api/<AuthController>/Register
        [HttpPost("Register")]
        public async Task<ActionResult<Customer>> Register(RegisterDto request)
        {
            string lowerCase = request.UserName.ToLower();
            if (await _repo.UserExists(lowerCase))
                return BadRequest("Tài khoản đã tồn tại");
            await _repo.Register(request);
            return StatusCode(201);
        }

    }
}
