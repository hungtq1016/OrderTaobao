
using BaseSource.BackendAPI.Authorization;
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
        public async Task<IActionResult> Get()
        {
            var result = await _imageService.Get();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("page")]
        public async Task<IActionResult> GetPagedData([FromQuery] PaginationRequest request)
        {
            var result = await _imageService.GetPagedData(request, Request.Path.Value!);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("upload")]
        [Permission]
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

        [HttpPost]
        [Permission]
        public async Task<IActionResult> Add(List<IFormFile> files)
        {
            var result = await _imageService.Add(files);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{path}")]
        [Permission]
        public IActionResult GetByPath(string path)
        {
            ImageResponse result = _imageService.GetByPath(path);

            if (result is null)
                return NotFound();

            return base.File(result.ImageBytes, $"image/{result.Extension}");
        }

        [HttpDelete("{id}")]
        [Permission]
        public async Task<IActionResult> Erase(string id)
        {
            var result = await _imageService.Erase(id);
            return StatusCode(result.StatusCode, result);

        }

    }

 
}
