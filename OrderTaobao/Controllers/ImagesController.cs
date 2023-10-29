
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ImagesController : StatusController
    {
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllImage()
        {
            return await PerformAction("", _imageService.ReadAllImages);
        }

        [HttpGet("page")]
        public async Task<IActionResult> GetPagedImage([FromQuery] PaginationRequest request)
        {
            var result = await _imageService.ReadPageImages(request, Request.Path.Value!, true);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("{user}")]
        public async Task<IActionResult> CreateImage(List<IFormFile> files, string user)
        {
            var result = await _imageService.CreateImage(files, user);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{name}")]
        [AllowAnonymous]
        public IActionResult GetImage(string name)
        {
            ImageResponse result = _imageService.ReadImage(name);

            if (result is null)
                return NotFound();

            return base.File(result.ImageBytes, $"image/{result.Extension}");
        }

        [HttpDelete("{id}/{name}")]
        public async Task<IActionResult> DeleteImage(string id,string name)
        {
            var result = await _imageService.DeleteImage(id,name);
            return StatusCode(result.StatusCode, result);

        }

    }

 
}
