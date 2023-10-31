using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _productService.Get();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _productService.GetById(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("{user}")]
        public async Task<IActionResult> Post(Product request,string user)
        {
            var result = await _productService.Add(request, user);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}/{user}")]
        public async Task<IActionResult> Update(string id,string user,Product request)
        {
            var result = await _productService.Update(id,user,request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("delete/{id}/{user}")]
        public async Task<IActionResult> Enable(string id, string user)
        {
            var result = await _productService.Enable(id, user,false);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("restore/{id}/{user}")]
        public async Task<IActionResult> Restore(string id, string user)
        {
            var result = await _productService.Enable(id, user, true);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("erase/{id}/{user}")]
        public async Task<IActionResult> Erase(string id, string user)
        {
            var result = await _productService.Erase(id, user);
            return StatusCode(result.StatusCode, result);
        }
    }
}
