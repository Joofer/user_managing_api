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
          if (_context.UserGroups == null)
          {
              return NotFound();
          }
            return await _context.UserGroups.ToListAsync();
        }

        // GET: api/UserGroup/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User_Group>> GetUser_Group(uint id)
        {
          if (_context.UserGroups == null)
          {
              return NotFound();
          }
            var user_Group = await _context.UserGroups.FindAsync(id);

            if (user_Group == null)
            {
                return NotFound();
            }

            return user_Group;
        }

        // PUT: api/UserGroup/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser_Group(uint id, User_Group user_Group)
        {
            if (id != user_Group.Id)
            {
                return BadRequest();
            }

            _context.Entry(user_Group).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!User_GroupExists(id))
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

        // POST: api/UserGroup
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User_Group>> PostUser_Group(User_Group user_Group)
        {
          if (_context.UserGroups == null)
          {
              return Problem("Entity set 'AppDbContext.UserGroups'  is null.");
          }
            _context.UserGroups.Add(user_Group);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser_Group", new { id = user_Group.Id }, user_Group);
        }

        // DELETE: api/UserGroup/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser_Group(uint id)
        {
            if (_context.UserGroups == null)
            {
                return NotFound();
            }
            var user_Group = await _context.UserGroups.FindAsync(id);
            if (user_Group == null)
            {
                return NotFound();
            }

            _context.UserGroups.Remove(user_Group);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool User_GroupExists(uint id)
        {
            return (_context.UserGroups?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
