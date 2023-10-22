using Microsoft.AspNetCore.Mvc;

namespace BaseSource.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /*  [Authorize]*/
    public class UsersController : StatusController
    {
        private readonly DataContext _context;
        private readonly IUserService _userService;
        public UsersController(DataContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] PaginationRequest request)
        {
            var result = await _userService.GetAllWithPagination(request, Request.Path.Value!);
            return StatusCode(result.StatusCode,result);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            return await PerformAction(id, _userService.GetUserDetailById);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, UserResponse request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            var user = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (user != null)
            {
                user.UserName = request.UserName;
                user.PhoneNumber = request.Phone;
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Email = request.Email;
            }
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(RegisterRequest request)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'DataContext.Users'  is null.");
            }
            User user = new()
            {
                Id = Guid.NewGuid().ToString(),
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                FirstName = request.FirstName,
                PhoneNumber = request.Phone,
                LastName = request.LastName,
                RefreshToken = Guid.NewGuid().ToString(),
                RefreshTokenExpiryTime = DateTime.Now.AddDays(7)
            };
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(string id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
