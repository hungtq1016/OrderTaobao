using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseSource.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorizeController : ControllerBase
    {
        // GET: api/<AuthorizeController>
        [HttpGet]
        [Route("customer")]
        public bool IsCustomer()
        {
            return true;
        }

        // GET api/<AuthorizeController>/5
        [HttpGet]
        [Route("admin")]
        public bool IsAdmin()
        {
            return true;
        }

    }
}
