using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BaseSource.Helper;
namespace BaseSource.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  /*  [Authorize]*/
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IUriService _uriService;
        public UsersController(DataContext context, UserManager<User> userManager, IUriService uriService)
        {
            _context = context;
            _userManager = userManager;
            _uriService = uriService;
        }

        // GET: api/Users
        [HttpGet]
        public  async Task<ActionResult> GetUsers([FromQuery] PaginationRequest request)
        {
            var route = Request.Path.Value;
            if (_context.Users == null)
              {
                  return NotFound();
              }
            var validFilter = new PaginationRequest(request.PageNumber, request.PageSize);
            var totalRecords = await _context.Users.CountAsync();
            var users = await _context.Users.Select(u=>new UserResponse
            {
                Id = u.Id,  
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Phone = u.PhoneNumber,
                UserName = u.UserName
            }).Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize).ToListAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<UserResponse>(users, validFilter, totalRecords, _uriService, route);
            return Ok(pagedReponse);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(string id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.Where(u=>u.Id==id).Include(u=>u.Orders).ThenInclude(o=>o.Details).Include(u => u.Notifications).FirstOrDefaultAsync();
            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);
            var orders = await _context.Orders.Where(o => o.UserId == id).ToListAsync();
            var notifications = await _context.Notifications.Where(o => o.UserId == id).ToListAsync();

            UserDetailResponse u = new UserDetailResponse {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Phone = user.PhoneNumber,  
                FirstName = user.FirstName,
                LastName = user.LastName,
                Enable = user.Enable,
                EmailConfirmed = user.EmailConfirmed,         
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled,             
            };
 
            return Ok(new Response<UserShowResponse>
            {
                Data = new UserShowResponse
                {
                    User = u,
                    Notifications = notifications,
                    Orders = orders,
                    Roles = roles
                },
                Message  = "Thành Công", 
                StatusCode = 200
            });
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
                user.UserName =  request.UserName;
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
