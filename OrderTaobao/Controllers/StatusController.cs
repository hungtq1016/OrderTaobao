using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.BackendAPI.Controllers
{
    public class StatusController : ControllerBase
    {
        public async Task<IActionResult> PerformAction<TRequest, TResult>(TRequest request, Func<TRequest, Task<Response<TResult>>> action)
        {
            if (request is null)
            {
                return BadRequest();
            }

            var result = action(request).Result;
            return StatusCode(result.StatusCode, result);
        }
    }
}
