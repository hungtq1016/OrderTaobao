namespace AudioService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionsController : ResourceController<Collection, CollectionRequest, CollectionResponse>
    {

        public CollectionsController(IService<Collection, CollectionRequest, CollectionResponse> service) : base(service)
        {
        }
    }
}
