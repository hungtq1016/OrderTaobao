using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using OfficeOpenXml;
using OfficeOpenXml.Table;

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
            var result = await _userService.GetPagedData(request, Request.Path.Value!,true);
            return StatusCode(result.StatusCode,result);
        }

        // GET: api/Users/Delete
        [HttpGet("Delete")]
        [Authorize(Roles = "Admin, Super Admin")]
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
        public async Task<IActionResult> PostUser(UserRequest request)
        {
            return await PerformAction(request, _userService.StoreUser);
        }

        // DELETE: api/Users/single/delete/5
        [HttpDelete("single/delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            return await PerformAction(id, _userService.DeleteUser);
        }

        // DELETE: api/Users/multiple/delete
        [HttpDelete("multiple/delete")]
        public async Task<IActionResult> DeleteMultipleUser(List<string> ids)
        {
            return await PerformAction(ids, _userService.DeleteAllUser);
        }

        // DELETE: api/Users/5/Delete
        [HttpDelete("single/erase/{id}")]
        [Authorize(Roles = "Admin, Super Admin")]
        public async Task<IActionResult> EraseUser(string id)
        {
            return await PerformAction(id, _userService.AbsoluteDeleteUser);
        }

        [HttpDelete("multiple/erase")]
        [Authorize(Roles = "Admin, Super Admin")]
        public async Task<IActionResult> EraseAllUsers(List<string> ids)
        {
            return await PerformAction(ids, _userService.AbsoluteDeleteAllUser);
        }
    }
}
