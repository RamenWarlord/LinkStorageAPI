using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LinkStorageAPI.Models;

namespace LinkStorageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly LinkStorageContext _context;

        public UsersController(LinkStorageContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if(_context.Users == null)
            {
                Response response = new Response();
                response.StatusCode = 404;
                response.StatusDescription = "Error NotFound: Users does not exist";
                response.Result = "";
                return NotFound(response);
            }

            return Ok(new Response(200, "Success: Users exist", await _context.Users.ToListAsync()));
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            Response response = new Response();
            if (_context.Users == null)
            {   
                response.StatusCode = 404;
                response.StatusDescription = "Error NotFound: Users does not exist";
                response.Result = "";
                return NotFound(response);
            }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                response.StatusCode = 404;
                response.StatusDescription = "Error NotFound: User with id: " + id +  " does not exist";
                response.Result = "";
                return NotFound(response);
            }

            response.StatusCode = 200;
            response.StatusDescription = "Success: User with id: " + id + " found";
            response.Result = user;
            return Ok(response);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest(new Response(400, "Error: Request Invalid/Corrupted. Try again later.", null));
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
                    return NotFound(new Response(404, "Error NotFound: Id does not exist", null));
                }
                else
                {
                    throw;
                }
            }

            return Ok(new Response(200, "Success: Change was posted.", CreatedAtAction("GetUser", new { id = user.UserId }, user).Value));
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (_context.Users == null)
            {
                Response response = new Response();
                response.StatusCode = 404;
                response.StatusDescription = "Error NotFound: Users does not exist";
                response.Result = "";
                return NotFound(response);
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return StatusCode(201, new Response(201, "Success: User was created.", CreatedAtAction("GetUser", new { id = user.UserId }, user).Value));
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                Response response = new Response();
                response.StatusCode = 404;
                response.StatusDescription = "Error NotFound: Users does not exist";
                response.Result = "";
                return NotFound(response);
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new Response(404, "Error NotFound: Users does not exist", null));
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new Response(200, "Success: User was deleted.", ""));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
