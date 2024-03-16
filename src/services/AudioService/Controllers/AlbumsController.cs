namespace AudioService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : SingletonController<Album, AlbumRequest, AlbumResponse>
    {

        public AlbumsController(IService<Album, AlbumRequest, AlbumResponse> service) : base(service)
        {
        }
    }
}
