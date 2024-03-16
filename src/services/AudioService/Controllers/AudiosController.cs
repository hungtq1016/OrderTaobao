namespace AudioService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudiosController : FileController<Audio, AudioRequest, AudioResponse, AudioExtensionEnums>
    {
        private readonly IAudioService _service;

        public AudiosController(IAudioService service) : base(service)
        {
            _service = service;
        }

        [HttpGet("{path}")]
        public IActionResult GetByPath(string path)
        {
            FileResponse result = _service.FindByPath(path);

            if (result is null)
                return NotFound();

            return base.File(result.FilesBytes, "audio/mpeg", $"{typeof(Audio).Name}/{result.Extension}");
        }
    }
}
