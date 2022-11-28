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
    public class ListsController : ControllerBase
    {
        private readonly LinkStorageContext _context;

        public ListsController(LinkStorageContext context)
        {
            _context = context;
        }

        // GET: api/Lists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lists>>> GetLists()
        {
            if (_context.Lists == null)
            {
                return NotFound(new Response(404, "Error NotFound: Lists do not exist", null));
            }
            return Ok(new Response(200, "Success: Lists found", await _context.Lists.ToListAsync()));
        }

        // GET: api/Lists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lists>> GetLists(int id)
        {
            if (_context.Lists == null)
            {
                return NotFound(new Response(404, "Error NotFound: Lists do not exist", null));
            }
            var lists = await _context.Lists.FindAsync(id);

            if (lists == null)
            {
                return NotFound(new Response(404, "Error NotFound: List does not exist", null));
            }

            return Ok(new Response(200, "Success: List found", lists));
        }

        // PUT: api/Lists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLists(int id, Lists lists)
        {
            if (id != lists.ListId)
            {
                return BadRequest(new Response(400, "Error: Request Invalid/Corrupted. Try again later.", null));
            }

            _context.Entry(lists).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListsExists(id))
                {
                    return NotFound(new Response(404, "Error NotFound: list does not exist", null));
                }
                else
                {
                    throw;
                }
            }

            return Ok(new Response(200, "Success: Change was posted.", CreatedAtAction("GetLists", new { id = lists.ListId }, lists).Value));
        }

        // POST: api/Lists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lists>> PostLists(Lists lists)
        {
            if (_context.Lists == null)
            {
                return NotFound(new Response(404, "Error NotFound: Lists do not exist", null));
            }
            _context.Lists.Add(lists);
            await _context.SaveChangesAsync();

            return StatusCode(201, new Response(201, "Success: List was created.", CreatedAtAction("GetLists", new { id = lists.ListId }, lists).Value));
        }

        // DELETE: api/Lists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLists(int id)
        {
            if (_context.Lists == null)
            {
                return NotFound(new Response(404, "Error NotFound: Lists do not exist", null));
            }
            var lists = await _context.Lists.FindAsync(id);
            if (lists == null)
            {
                return NotFound(new Response(404, "Error NotFound: List does not exist", null));
            }

            _context.Lists.Remove(lists);
            await _context.SaveChangesAsync();

            return Ok(new Response(200, "Success: List was deleted.", ""));
        }

        private bool ListsExists(int id)
        {
            return _context.Lists.Any(e => e.ListId == id);
        }
    }
}
