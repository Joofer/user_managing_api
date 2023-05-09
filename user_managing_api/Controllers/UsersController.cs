using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using user_managing_api.Models;

namespace user_managing_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users
                .Include(u => u.User_Group)
                .Include(u => u.User_State)
                .ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(uint id)
        {
            var _user = await _context.Users.FindAsync(id);
            if (_user == null) return NotFound();
            await _context.Entry(_user).Reference(u => u.User_Group).LoadAsync();
            await _context.Entry(_user).Reference(u => u.User_State).LoadAsync();
            return _user;
        }

        // PUT: api/Users/5
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutUser(uint id, DTO_User user)
        {
            if (id != user.Id) return BadRequest();

            var _user = await _context.Users.FindAsync(id);
            var _group = await _context.UserGroups.FindAsync(user.User_Group_Id);
            var _state = await _context.UserStates.FindAsync(user.User_State_Id);
            if (_user == null || _group == null || _state == null) return NotFound();
            _user.Login = user.Login;
            _user.Password = user.Password;
            // CreatedDate cannot be changed
            // _user.CreatedDate = user.CreatedDate;
            _user.User_GroupId = _group.Id;
            _user.User_Group = _group;
            _user.User_StateId = _state.Id;
            _user.User_State = _state;

            // Check for admin group
            if (_group.Code == "Admin" && await AdminExists())
                return Problem("Another User with group 'Admin' already exists.");

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }*/

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(DTO_User user)
        {
            var _group = await _context.UserGroups.FindAsync(user.User_Group_Id);
            // Get Active state
            var _state = await _context.UserStates.Where(u => u.Code == "Active").FirstOrDefaultAsync();
            if (_group == null || _state == null) return NotFound();
            var _user = new User()
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                // Assign date
                CreatedDate = DateTime.UtcNow,
                User_GroupId = _group.Id,
                User_Group = _group,
                // Assign Active state
                User_StateId = _state.Id,
                User_State = _state
            };

            // Check for login
            if (await LoginExists(_user.Login))
                return Problem($"Another User with login '{_user.Login}' is being created.");

            // Check for admin group
            if (_group.Code == "Admin" && await AdminExists())
                return Problem("Another User with group 'Admin' already exists.");

            try
            {
                _context.Users.Add(_user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return Problem(ex.InnerException.Message);
                return Problem(ex.Message);
            }

            return CreatedAtAction("GetUser", new { id = _user.Id }, _user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(uint id)
        {
            var _user = await _context.Users.FindAsync(id);
            if (_user == null) return NotFound();
            _user.User_State = await _context.UserStates
                .Where(u => u.Code == "Blocked")
                .FirstOrDefaultAsync();

            try
            {
                _context.Users.Remove(_user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return Problem(ex.InnerException.Message);
                return Problem(ex.Message);
            }

            return NoContent();
        }

        private bool UserExists(uint id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task<bool> LoginExists(string login) =>
            await _context.Users.Where(u => u.Login == login).Where(u => (DateTime.UtcNow - u.CreatedDate!.Value).Seconds <= 5).AnyAsync();

        private async Task<bool> AdminExists() =>
            await _context.Users.Where(u => u.User_Group!.Code == "Admin").AnyAsync();

        private static DTO_User ToDTO(User user) =>
        new()
        {
            Id = user.Id,
            Login = user.Login,
            Password = user.Password,
            User_Group_Id = user.User_GroupId
        };
    }
}
