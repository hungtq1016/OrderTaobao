using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Super Admin,Manager")]
    public class UsersController : StatusController
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] PaginationRequest request)
        {
            var result = await _userService.GetAllWithPagination(request, Request.Path.Value!,true);
            return StatusCode(result.StatusCode,result);
        }

        // GET: api/Users/Delete
        [HttpGet("Delete")]
        [Authorize(Roles = "Admin, Super Admin")]
        public async Task<IActionResult> GetDeleteUsers([FromQuery] PaginationRequest request)
        {
            var result = await _userService.GetAllWithPagination(request, Request.Path.Value!,false);
            return StatusCode(result.StatusCode, result);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            return await PerformAction(id, _userService.GetUserDetailById);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id,UserRequest request)
        {
            var result = await _userService.UpdateUser(id,request);
            return StatusCode(result.StatusCode, result);
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostUser(UserRequest request)
        {
            return await PerformAction(request, _userService.StoreUser);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            return await PerformAction(id, _userService.DeleteUser);
        }

        // DELETE: api/Users/5/Delete
        [HttpDelete("{id}/Delete")]
        [Authorize(Roles = "Admin, Super Admin")]
        public async Task<IActionResult> AbsoluteDeleteUser(string id)
        {
            return await PerformAction(id, _userService.AbsoluteDeleteUser);
        }
    }
}
