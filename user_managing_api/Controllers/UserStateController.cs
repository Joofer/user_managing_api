using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
          if (_context.UserStates == null)
          {
              return NotFound();
          }
            return await _context.UserStates.ToListAsync();
        }

        // GET: api/UserState/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User_State>> GetUser_State(uint id)
        {
          if (_context.UserStates == null)
          {
              return NotFound();
          }
            var user_State = await _context.UserStates.FindAsync(id);

            if (user_State == null)
            {
                return NotFound();
            }

            return user_State;
        }

        // PUT: api/UserState/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser_State(uint id, User_State user_State)
        {
            if (id != user_State.Id)
            {
                return BadRequest();
            }

            _context.Entry(user_State).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!User_StateExists(id))
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

        // POST: api/UserState
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User_State>> PostUser_State(User_State user_State)
        {
          if (_context.UserStates == null)
          {
              return Problem("Entity set 'AppDbContext.UserStates'  is null.");
          }
            _context.UserStates.Add(user_State);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser_State", new { id = user_State.Id }, user_State);
        }

        // DELETE: api/UserState/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser_State(uint id)
        {
            if (_context.UserStates == null)
            {
                return NotFound();
            }
            var user_State = await _context.UserStates.FindAsync(id);
            if (user_State == null)
            {
                return NotFound();
            }

            _context.UserStates.Remove(user_State);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool User_StateExists(uint id)
        {
            return (_context.UserStates?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
