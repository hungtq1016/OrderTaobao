using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "UserView")]
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
            var result = await _userService.GetPagedData(request, Request.Path.Value!,true);
            return StatusCode(result.StatusCode,result);
        }

        // GET: api/Users/disabled
        [HttpGet("disable")]
        [Authorize(Policy = "DeleteView")]
        public async Task<IActionResult> GetDeletedUsers([FromQuery] PaginationRequest request)
        {
            var result = await _userService.GetPagedData(request, Request.Path.Value!,false);
            return StatusCode(result.StatusCode, result);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            return await PerformAction(id, _userService.GetById);
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
        [Authorize(Policy = "UserEdit")]
        public async Task<IActionResult> PostUser(UserRequest request)
        {
            return await PerformAction(request, _userService.StoreUser);
        }

        // DELETE: api/Users/single/delete/5
        [HttpDelete("single/delete/{id}")]
        [Authorize(Policy = "UserDelete")]
        public async Task<IActionResult> DeleteSingleUser(string id)
        {
            var result = await _userService.UpdateEnable(id, false);
            return StatusCode(result.StatusCode, result);
        }

        // PUT: api/Users/single/Restore/5
        [HttpPut("single/restore/{id}")]
        [Authorize(Policy = "UserDelete")]
        public async Task<IActionResult> RestoreSingleUser(string id)
        {
            var result = await _userService.UpdateEnable(id, true);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE: api/Users/multiple/delete
        [HttpDelete("multiple/delete")]
        [Authorize(Policy = "UserDelete")]
        public async Task<IActionResult> DeleteMultipleUser(List<string> ids)
        {
            var result = await _userService.UpdateEnableMultipleUser(ids, false);
            return StatusCode(result.StatusCode, result);
        }

        // PUT: api/Users/multiple/Restore
        [HttpPut("multiple/restore")]
        [Authorize(Policy = "UserDelete")]
        public async Task<IActionResult> RestoreMultipleUser(List<string> ids)
        {
            var result = await _userService.UpdateEnableMultipleUser(ids, true);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE: api/Users/5/Delete
        [HttpDelete("single/erase/{id}")]
        [Authorize(Policy = "UserDelete")]
        public async Task<IActionResult> EraseSingleUser(string id)
        {
            return await PerformAction(id, _userService.EraseUser);
        }

        [HttpDelete("multiple/erase")]
        [Authorize(Policy = "UserDelete")]
        public async Task<IActionResult> EraseMultipleUsers(List<string> ids)
        {
            return await PerformAction(ids, _userService.EraseMultipleUser);
        }
    }
}
