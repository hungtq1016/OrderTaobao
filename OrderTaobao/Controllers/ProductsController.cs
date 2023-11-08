using BaseSource.BackendAPI.Authorization;
using BaseSource.Dto.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IService<Product> _service;
        public ProductsController(IService<Product> service)
        {
            _service = service;
        }

        // GET: api/Products/page
        [HttpGet("page")]
        public async Task<IActionResult> GetPagedData([FromQuery] PaginationRequest request)
        {
            var result = await _service.GetPagedData(request, Request.Path.Value!, true);
            return StatusCode(result.StatusCode, result);
        }

        // GET: api/Products/page-disable
        [HttpGet("page-disable")]
        [ClaimRequirement("permission", "disable.view")]
        public async Task<IActionResult> GetPagedDisableData([FromQuery] PaginationRequest request)
        {
            var result = await _service.GetPagedData(request, Request.Path.Value!, false);
            return StatusCode(result.StatusCode, result);
        }

        // GET: api/Products
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var result = await _service.Get();
            return StatusCode(result.StatusCode, result);
        }

        // GET: api/Products/123
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _service.GetById(id);
            return StatusCode(result.StatusCode, result);
        }

        // POST: api/Products/usersubmit
        [HttpPost("{user}")]
        [ClaimRequirement("permission", "product.add")]
        public async Task<IActionResult> Post(Product request, string user)
        {
            var result = await _service.Add(user, request);
            return StatusCode(result.StatusCode, result);
        }

        // PUT: api/Products/123/usersubmit
        [HttpPut("{id}/{user}")]
        [ClaimRequirement("permission", "product.edit")]
        public async Task<IActionResult> Update(string id, string user, Product request)
        {
            var result = await _service.Update(id, user, request);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE: api/Products/123/usersubmit
        [HttpPut("disable/{id}/{user}")]
        [ClaimRequirement("permission", "product.edit")]
        public async Task<IActionResult> Disable(string id, string user)
        {
            var result = await _service.Enable(id, user, false);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE: api/Products
        [HttpPut("disable/multiple")]
        [ClaimRequirement("permission", "product.edit")]
        public async Task<IActionResult> MultipleDisable(MultipleRequest request)
        {
            var result = await _service.MultipleEnable(request, false);
            return StatusCode(result.StatusCode, result);
        }

        // PUT: api/Products/restore/123/usersubmit
        [HttpPut("restore/{id}/{user}")]
        [ClaimRequirement("permission", "product.edit")]
        public async Task<IActionResult> Restore(string id, string user)
        {
            var result = await _service.Enable(id, user, true);
            return StatusCode(result.StatusCode, result);
        }

        // PUT: api/Products/delete/multiple
        [HttpPut("delete/multiple")]
        [ClaimRequirement("permission", "product.edit")]
        public async Task<IActionResult> MultipleRestore(MultipleRequest request)
        {
            var result = await _service.MultipleEnable(request, true);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE: api/Products/erase/123/usersubmit
        [HttpDelete("erase/{id}")]
        [ClaimRequirement("permission", "product.delete")]
        public async Task<IActionResult> Erase(string id)
        {
            var result = await _service.Erase(id);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE: api/Products/erase/multiple
        [HttpDelete("erase/multiple")]
        [ClaimRequirement("permission", "product.delete")]
        public async Task<IActionResult> MultipleErase(MultipleRequest request)
        {
            var result = await _service.MultipleErase(request);
            return StatusCode(result.StatusCode, result);
        }


        [HttpGet("excel")]
        // GET: api/Products/excel
        public async Task<IActionResult> Export()
        {
           var export = await _service.Export();

           if (export is null)
                return NotFound();

            return File(export.File,export.Extension, export.Name);
        }


        [HttpPost("excel")]
        // POST: api/Products/excel
        public async Task<IActionResult> Import(IFormFile file)
        {
            var result = await _service.Import(file);
            return Ok(result);
        }
    }
}
