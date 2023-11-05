
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : StatusController
    {
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _imageService.Get();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("page")]
        public async Task<IActionResult> GetPagedData([FromQuery] PaginationRequest request)
        {
            var result = await _imageService.GetPagedData(request, Request.Path.Value!, true);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.GetTempFileName();

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size });
        }

        [HttpPost("{user}")]
        public async Task<IActionResult> Add(List<IFormFile> files, string user)
        {
            var result = await _imageService.Add(files, user);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{path}")]
        [AllowAnonymous]
        public IActionResult GetByPath(string path)
        {
            ImageResponse result = _imageService.GetByPath(path);

            if (result is null)
                return NotFound();

            return base.File(result.ImageBytes, $"image/{result.Extension}");
        }

        [HttpDelete("{id}/{name}")]
        public async Task<IActionResult> Erase(string id,string name)
        {
            var result = await _imageService.Erase(id,name);
            return StatusCode(result.StatusCode, result);

        }

    }

 
}
