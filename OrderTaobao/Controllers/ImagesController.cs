using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageService _imageService;
        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllImage()
        {
            var result = await _imageService.ReadAllImages();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateImage(List<IFormFile> files)
        {
            var result = await _imageService.CreateImage(files);
            return Ok(result);
        }

        [HttpGet("{name}")]
        public IActionResult GetImage(string name)
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload/Image", name);

            if (System.IO.File.Exists(imagePath))
            {
                var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                return File(imageBytes, "image/*"); 
            }
            else
            {
                return NotFound(); 
            }
        }

    }

 
}
