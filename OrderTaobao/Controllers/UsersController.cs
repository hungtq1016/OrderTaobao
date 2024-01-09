using BaseSource.BackendAPI.Authorization;
using BaseSource.Dto.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BaseSource.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : StatusController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users/page
        [HttpGet]
        public async Task<IActionResult> GetPagedData([FromQuery] PaginationRequest request)
        {
            var result = await _userService.GetPagedData(request, Request.Path.Value!);
            return StatusCode(result.StatusCode,result);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return await PerformAction(id, _userService.GetById);
        }

        // POST: api/Users
        [HttpPost]
        [Permission]
        public async Task<IActionResult> Post(UserRequest request)
        {
            return await PerformAction(request, _userService.Add);
        }

        // PUT: api/Users/123
        [HttpPut("{id}")]
        [Permission]
        public async Task<IActionResult> Put(string id,[FromBody] UserRequest request)
        {
            var result = await _userService.Update(id,request);
            return StatusCode(result.StatusCode, result);
        }

        // PUT: api/Users
        [HttpPut]
        [Permission]
        public async Task<IActionResult> BulkPut(MultipleRequest request)
        {
            var result = await _userService.MultipleUpdate(request);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE: api/Users/erase/123
        [HttpDelete("{id}")]
        [Permission]
        public async Task<IActionResult> Delete(string id)
        {
            return await PerformAction(id, _userService.Erase);
        }

        // DELETE: api/Users/erase/multiple
        [HttpDelete]
        [Permission]
        public async Task<IActionResult> BulkDelete(MultipleRequest request)
        {
            return await PerformAction(request, _userService.MultipleErase);
        }

        [HttpGet("excel")]
        // GET: api/Products/excel
        public async Task<IActionResult> Export()
        {
            var export = await _userService.Export();

            if (export is null)
                return NotFound();

            return File(export.File, export.Extension, export.Name);
        }


        [HttpPost("excel")]
        [AllowAnonymous]
        // POST: api/Products/excel
        public async Task<IActionResult> Import(IFormFile file)
        {
            var result = await _userService.Import(file);
            return Ok(result);
        }
    }
}
