using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
        {
            var result = await WriteFile(files);
            return Ok(result);
        }
        private async Task<List<string>> WriteFile(List<IFormFile> files)
        {
            List<string> filename = new List<string>();
            foreach (var file in files)
            {
                
                try
                {
                    var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                    string fileName = DateTime.Now.Ticks.ToString() + extension;

                    var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");

                    if (!Directory.Exists(filepath))
                    {
                        Directory.CreateDirectory(filepath);
                    }

                    var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", fileName);
                    using (var stream = new FileStream(exactpath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        filename.Add(fileName);
                    }
                }
                catch (Exception ex)
                {
                }
            }
            
            return filename;
        }

    }

 
}
