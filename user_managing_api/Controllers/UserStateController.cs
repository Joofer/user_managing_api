using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using user_managing_api.Models;

namespace user_managing_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserStateController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserStateController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserState
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User_State>>> GetUserStates()
        {
            return await _context.UserStates
                .Select(u => u)
                .ToListAsync();
        }

        // GET: api/UserState/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User_State>> GetUser_State(uint id)
        {
            var _user_State = await _context.UserStates.FindAsync(id);
            if (_user_State == null) return NotFound();
            return _user_State;
        }

        // PUT: api/UserState/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser_State(uint id, User_State user_State)
        {
            if (id != user_State.Id) return BadRequest();

            _context.Entry(user_State).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!User_StateExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // POST: api/UserState
        [HttpPost]
        public async Task<ActionResult<User_State>> PostUser_State(User_State user_State)
        {
            try
            {
                _context.UserStates.Add(user_State);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return Problem(ex.InnerException.Message);
                return Problem(ex.Message);
            }

            return CreatedAtAction("GetUser_State", new { id = user_State.Id }, user_State);
        }

        // DELETE: api/UserState/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser_State(uint id)
        {
            var user_State = await _context.UserStates.FindAsync(id);
            if (user_State == null) return NotFound();

            try
            {
                _context.UserStates.Remove(user_State);
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

        private bool User_StateExists(uint id)
        {
            return (_context.UserStates?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
