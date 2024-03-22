namespace AudioService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionsController : ResourceController<Collection, CollectionRequest, CollectionResponse>
    {
        private readonly IService<Collection, CollectionRequest, CollectionResponse> _service;

        public CollectionsController(IService<Collection, CollectionRequest, CollectionResponse> service) : base(service)
        {
            _service = service;
        }

        [HttpGet("{id:Guid}")]
        public override async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.FindByParamsAsync(id,properties: new string[] { "Albums.Audio" });
            return StatusCode(result.StatusCode, result);
        }
    }
}
