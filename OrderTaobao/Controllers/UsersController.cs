using BaseSource.BackendAPI.Authorization;
using BaseSource.Dto.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ClaimRequirement("permission", "user.view")]
    public class UsersController : StatusController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users/page
        [HttpGet("page")]
        public async Task<IActionResult> GetPagedData([FromQuery] PaginationRequest request)
        {
            var result = await _userService.GetPagedData(request, Request.Path.Value!,true);
            return StatusCode(result.StatusCode,result);
        }

        // GET: api/Users/page-disable
        [HttpGet("page-disable")]
        [Authorize(Policy = "DeleteView")]
        public async Task<IActionResult> GetPagedDisableData([FromQuery] PaginationRequest request)
        {
            var result = await _userService.GetPagedData(request, Request.Path.Value!,false);
            return StatusCode(result.StatusCode, result);
        }

        // GET: api/Users
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var result = await _userService.Get();
            return StatusCode(result.StatusCode, result);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return await PerformAction(id, _userService.GetById);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/{user}")]
        public async Task<IActionResult> Update(string id,string user,UserRequest request)
        {
            var result = await _userService.Update(id,request);
            return StatusCode(result.StatusCode, result);
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = "UserEdit")]
        public async Task<IActionResult> Add(UserRequest request)
        {
            return await PerformAction(request, _userService.Add);
        }

        // DELETE: api/Users/delete/usersubmit
        [HttpDelete("delete/{id}/{user}")]
        [Authorize(Policy = "UserDelete")]
        public async Task<IActionResult> Enable(string id,string user)
        {
            var result = await _userService.Enable(id, false);
            return StatusCode(result.StatusCode, result);
        }

        // PUT: api/Users/Restore/5
        [HttpPut("restore/{id}/{user}")]
        [Authorize(Policy = "UserDelete")]
        public async Task<IActionResult> Restore(string id,string user)
        {
            var result = await _userService.Enable(id, true);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE: api/Users/delete/multiple
        [HttpDelete("delete/multiple")]
        [Authorize(Policy = "UserDelete")]
        public async Task<IActionResult> MultipleEnable(MultipleRequest request)
        {
            var result = await _userService.MultipleEnable(request, false);
            return StatusCode(result.StatusCode, result);
        }

        // PUT: api/Users/Restore/multiple
        [HttpPut("restore/multiple")]
        [Authorize(Policy = "UserDelete")]
        public async Task<IActionResult> MultipleRestore(MultipleRequest request)
        {
            var result = await _userService.MultipleEnable(request, true);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE: api/Users/erase/123
        [HttpDelete("erase/{id}")]
        [Authorize(Policy = "UserDelete")]
        public async Task<IActionResult> Erase(string id)
        {
            return await PerformAction(id, _userService.Erase);
        }

        // DELETE: api/Users/multiple/erase
        [HttpDelete("multiple/erase")]
        [Authorize(Policy = "UserDelete")]
        public async Task<IActionResult> MultipleErase(MultipleRequest request)
        {
            return await PerformAction(request, _userService.MultipleErase);
        }
    }
}
