using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using user_managing_api.Models;

namespace user_managing_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserGroupController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserGroup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User_Group>>> GetUserGroups()
        {
            return await _context.UserGroups
                .Select(u => u)
                .ToListAsync();
        }

        // GET: api/UserGroup/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User_Group>> GetUser_Group(uint id)
        {
            var _user_Group = await _context.UserGroups.FindAsync(id);
            if (_user_Group == null) return NotFound();
            return _user_Group;
        }

        // PUT: api/UserGroup/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser_Group(uint id, User_Group user_Group)
        {
            if (id != user_Group.Id) return BadRequest();

            _context.Entry(user_Group).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!User_GroupExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // POST: api/UserGroup
        [HttpPost]
        public async Task<ActionResult<User_Group>> PostUser_Group(User_Group user_Group)
        {
            try
            {
                _context.UserGroups.Add(user_Group);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return Problem(ex.InnerException.Message);
                return Problem(ex.Message);
            }

            return CreatedAtAction("GetUser_Group", new { id = user_Group.Id }, user_Group);
        }

        // DELETE: api/UserGroup/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser_Group(uint id)
        {
            var _user_Group = await _context.UserGroups.FindAsync(id);
            if (_user_Group == null) return NotFound();

            try
            {
                _context.UserGroups.Remove(_user_Group);
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

        private bool User_GroupExists(uint id)
        {
            return (_context.UserGroups?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
