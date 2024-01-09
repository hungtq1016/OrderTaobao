using BaseSource.BackendAPI.Authorization;
using BaseSource.Dto.Request;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IService<Category, CategoryRequest, CategoryResponse> _service;
        public CategoriesController(IService<Category, CategoryRequest, CategoryResponse> service)
        {
            _service = service;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<IActionResult> GetPagedData([FromQuery] PaginationRequest request)
        {
            var result = await _service.GetPagedData(request, Request.Path.Value!);
            return StatusCode(result.StatusCode, result);
        }


        // GET: api/Categories/123
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _service.GetById(id);
            return StatusCode(result.StatusCode, result);
        }

        // POST: api/Categories
        [HttpPost]
        [ClaimRequirement("permission", "category.add")]
        public async Task<IActionResult> Post([FromBody] CategoryRequest request)
        {
            var result = await _service.Add(request);
            return StatusCode(result.StatusCode, result);
        }

        // PUT: api/Categories/123
        [HttpPut("{id}")]
        /*[ClaimRequirement("permission", "category.edit")]*/
        public async Task<IActionResult> Update(string id,[FromBody] CategoryRequest request)
        {
            var result = await _service.Update(id, request);
            return StatusCode(result.StatusCode, result);
        }

        // PUT: api/Categories
        [HttpPut]
        [ClaimRequirement("permission", "category.edit")]
        public async Task<IActionResult> MultipleDisable(MultipleRequest request)
        {
            var result = await _service.MultipleUpdate(request);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE: api/Categories/123
        [HttpDelete("{id}")]
        [ClaimRequirement("permission", "category.delete")]
        public async Task<IActionResult> Erase(string id)
        {
            var result = await _service.Erase(id);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE: api/Categories/multiple
        [HttpDelete]
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
