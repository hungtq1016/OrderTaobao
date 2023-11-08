using BaseSource.BackendAPI.Authorization;
using BaseSource.Dto.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly IService<Category> _service;
        public CategoriesController(IService<Category> service)
        {
            _service = service;
        }

        // GET: api/Categories/page
        [HttpGet("page")]
        public async Task<IActionResult> GetPagedData([FromQuery] PaginationRequest request)
        {
            var result = await _service.GetPagedData(request, Request.Path.Value!, true);
            return StatusCode(result.StatusCode, result);
        }

        // GET: api/Categories/page-disable
        [HttpGet("page-disable")]
        [ClaimRequirement("permission", "disable.view")]
        public async Task<IActionResult> GetPagedDisableData([FromQuery] PaginationRequest request)
        {
            var result = await _service.GetPagedData(request, Request.Path.Value!, false);
            return StatusCode(result.StatusCode, result);
        }

        // GET: api/Categories
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var result = await _service.Get();
            return StatusCode(result.StatusCode, result);
        }

        // GET: api/Categories/123
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _service.GetById(id);
            return StatusCode(result.StatusCode, result);
        }

        // POST: api/Categories/usersubmit
        [HttpPost("{user}")]
        [ClaimRequirement("permission", "category.add")]
        public async Task<IActionResult> Post(Category request, string user)
        {
            var result = await _service.Add(user, request);
            return StatusCode(result.StatusCode, result);
        }

        // PUT: api/Categories/123/usersubmit
        [HttpPut("{id}/{user}")]
        [ClaimRequirement("permission", "category.edit")]
        public async Task<IActionResult> Update(string id, string user, Category request)
        {
            var result = await _service.Update(id, user, request);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE: api/Categories/123/usersubmit
        [HttpPut("disable/{id}/{user}")]
        [ClaimRequirement("permission", "category.edit")]
        public async Task<IActionResult> Disable(string id, string user)
        {
            var result = await _service.Enable(id, user, false);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE: api/Categories
        [HttpPut("disable/multiple")]
        [ClaimRequirement("permission", "category.edit")]
        public async Task<IActionResult> MultipleDisable(MultipleRequest request)
        {
            var result = await _service.MultipleEnable(request, false);
            return StatusCode(result.StatusCode, result);
        }

        // PUT: api/Categories/restore/123/usersubmit
        [HttpPut("restore/{id}/{user}")]
        [ClaimRequirement("permission", "category.edit")]
        public async Task<IActionResult> Restore(string id, string user)
        {
            var result = await _service.Enable(id, user, true);
            return StatusCode(result.StatusCode, result);
        }

        // PUT: api/Categories/delete/multiple
        [HttpPut("restore/multiple")]
        [ClaimRequirement("permission", "category.edit")]
        public async Task<IActionResult> MultipleRestore(MultipleRequest request)
        {
            var result = await _service.MultipleEnable(request, true);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE: api/Categories/erase/123/usersubmit
        [HttpDelete("erase/{id}")]
        [ClaimRequirement("permission", "category.delete")]
        public async Task<IActionResult> Erase(string id)
        {
            var result = await _service.Erase(id);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE: api/Categories/erase/multiple
        [HttpDelete("erase/multiple")]
        [ClaimRequirement("permission", "category.delete")]
        public async Task<IActionResult> MultipleErase(MultipleRequest request)
        {
            var result = await _service.MultipleErase(request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("excel")]
        // GET: api/Categories/excel
        public async Task<IActionResult> Export()
        {
            var export = await _service.Export();

            if (export is null)
                return NotFound();

            return File(export.File, export.Extension, export.Name);
        }


        [HttpPost("excel")]
        // POST: api/Categories/excel
        public async Task<IActionResult> Import(IFormFile file)
        {
            var result = await _service.Import(file);
            return Ok(result);
        }
    }
}
