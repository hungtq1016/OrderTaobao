using ProductService.Services;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ResourceController<Product, ProductRequest, ProductResponse>
    {
        private readonly IProductService _productService;
        public ProductsController(IService<Product, ProductRequest, ProductResponse> service, IProductService productService) : base(service)
        {
            _productService = productService;
        }

        [HttpGet("taobao/{url}")]
        public async Task<IActionResult> Taobao(string url)
        {
            var result = await _productService.GetProductByUrlAsync(url);
            return StatusCode(result.StatusCode, result);
        }
    }
}
