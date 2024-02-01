using Core;
using Infrastructure.EFCore.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.EFCore.Controllers
{
    public abstract class FileController<TEntity,TRequest,TResponse,TExtensionEnum> : ControllerBase where TEntity : AbstractFile where TExtensionEnum : Enum  where TRequest : EntityRequest
    {
        private readonly IFileService<TEntity, TRequest, TResponse, TExtensionEnum> _service;

        public FileController(IFileService<TEntity, TRequest, TResponse, TExtensionEnum> service)
        {
            _service = service;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            var result = await _service.FindAllAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post(List<IFormFile> files)
        {
            var result = await _service.AddAsync(files);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id:Guid}")]
        public virtual async Task<IActionResult> Put(Guid id, TRequest request)
        {
            var result = await _service.EditAsync(id, request);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id:Guid}")]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
