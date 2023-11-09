using BaseSource.BackendAPI.Authorization;
using BaseSource.Dto.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Table;
using OfficeOpenXml;

namespace BaseSource.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [ClaimRequirement("permission", "disable.view")]
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
        [HttpPut("{id}/{user}")]
        public async Task<IActionResult> Update(string id,string user,UserRequest request)
        {
            var result = await _userService.Update(id,request);
            return StatusCode(result.StatusCode, result);
        }

        // POST: api/Users
        [HttpPost]
        [ClaimRequirement("permission", "user.add")]
        public async Task<IActionResult> Add(UserRequest request)
        {
            return await PerformAction(request, _userService.Add);
        }

        // DELETE: api/Users/delete/usersubmit
        [HttpPut("disable/{id}/{user}")]
        [ClaimRequirement("permission", "user.edit")]
        public async Task<IActionResult> Disable(string id,string user)
        {
            var result = await _userService.Enable(id, false);
            return StatusCode(result.StatusCode, result);
        }

        // PUT: api/Users/Restore/5
        [HttpPut("restore/{id}/{user}")]
        [ClaimRequirement("permission", "user.edit")]
        public async Task<IActionResult> Restore(string id,string user)
        {
            var result = await _userService.Enable(id, true);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE: api/Users/delete/multiple
        [HttpPut("disable/multiple")]
        [ClaimRequirement("permission", "user.edit")]
        public async Task<IActionResult> MultipleDisable(MultipleRequest request)
        {
            var result = await _userService.MultipleEnable(request, false);
            return StatusCode(result.StatusCode, result);
        }

        // PUT: api/Users/Restore/multiple
        [HttpPut("restore/multiple")]
        [ClaimRequirement("permission", "user.edit")]
        public async Task<IActionResult> MultipleRestore(MultipleRequest request)
        {
            var result = await _userService.MultipleEnable(request, true);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE: api/Users/erase/123
        [HttpDelete("erase/{id}")]
        [ClaimRequirement("permission", "user.delete")]
        public async Task<IActionResult> Erase(string id)
        {
            return await PerformAction(id, _userService.Erase);
        }

        // DELETE: api/Users/erase/multiple
        [HttpDelete("erase/multiple")]
        [ClaimRequirement("permission", "user.delete")]
        public async Task<IActionResult> MultipleErase(MultipleRequest request)
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
