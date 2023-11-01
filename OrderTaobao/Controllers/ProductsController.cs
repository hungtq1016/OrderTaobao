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

        [HttpGet("page")]
        public async Task<IActionResult> GetPagedData([FromQuery] PaginationRequest request)
        {
            var result = await _service.GetPagedData(request, Request.Path.Value!, true);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("page-disable")]
        public async Task<IActionResult> GetPagedDisableData([FromQuery] PaginationRequest request)
        {
            var result = await _service.GetPagedData(request, Request.Path.Value!, false);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.Get();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _service.GetById(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("{user}")]
        public async Task<IActionResult> Post(Product request, string user)
        {
            var result = await _service.Add(user, request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}/{user}")]
        public async Task<IActionResult> Update(string id, string user, Product request)
        {
            var result = await _service.Update(id, user, request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("delete/{id}/{user}")]
        public async Task<IActionResult> Enable(string id, string user)
        {
            var result = await _service.Enable(id, user, false);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("restore/{id}/{user}")]
        public async Task<IActionResult> Restore(string id, string user)
        {
            var result = await _service.Enable(id, user, true);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("erase/{id}/{user}")]
        public async Task<IActionResult> Erase(string id, string user)
        {
            var result = await _service.Erase(id, user);
            return StatusCode(result.StatusCode, result);
        }
    }
}
