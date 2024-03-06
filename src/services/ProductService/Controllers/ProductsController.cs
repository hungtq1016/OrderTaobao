using Microsoft.AspNetCore.Mvc;

namespace OAuth2Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        /*private readonly IService<Product, Product, Product> _service;
        public ProductsController(IService<Product, Product, Product> service)
        {
            _service = service;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> GetPagedData([FromQuery] PaginationRequest request)
        {
            var result = await _service.GetPagedData(request, Request.Path.Value!);
            return StatusCode(result.StatusCode, result);
        }

        // GET: api/Products/123
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _service.GetById(id, properties: "Categories");
            return StatusCode(result.StatusCode, result);
        }

        // POST: api/Products
        [HttpPost]
      *//*  [Permission]*//*
        public async Task<IActionResult> Post(Product request)
        {
            var result = await _service.Add(request);
            return StatusCode(result.StatusCode, result);
        }

        // PUT: api/Products/123
        [HttpPut("{id}")]
        [Permission]
        public async Task<IActionResult> Update(string id, Product request)
        {
            var result = await _service.Update(id, request);
            return StatusCode(result.StatusCode, result);
        }

        // PUT: api/Products
        [HttpPut]
        [Permission]
        public async Task<IActionResult> MultipleUpdate(MultipleRequest request)
        {
            var result = await _service.MultipleUpdate(request);
            return StatusCode(result.StatusCode, result);
        }


        // DELETE: api/Products/erase/123
        [HttpDelete("{id}")]
        [Permission]
        public async Task<IActionResult> Erase(string id)
        {
            var result = await _service.Erase(id);
            return StatusCode(result.StatusCode, result);
        }

        // DELETE: api/Products
        [HttpDelete]
        [Permission]
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
        }*/
    }
}
