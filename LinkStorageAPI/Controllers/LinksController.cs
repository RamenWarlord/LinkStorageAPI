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
    public class LinksController : ControllerBase
    {
        private readonly LinkStorageContext _context;

        public LinksController(LinkStorageContext context)
        {
            _context = context;
        }

        // GET: api/Links
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Links>>> GetLinks()
        {
            if (_context.Links == null)
            {
                return NotFound(new Response(404, "Error NotFound: Links do not exist", null));
            }
            return Ok(new Response(200, "Success: Links found", await _context.Links.ToListAsync()));
        }

        // GET: api/Links/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Links>> GetLinks(int id)
        {
            if (_context.Links == null)
            {
                return NotFound(new Response(404, "Error NotFound: Links do not exist", null));
            }
            var links = await _context.Links.FindAsync(id);

            if (links == null)
            {
                return NotFound(new Response(404, "Error NotFound: Link does not exist", null));
            }

            return Ok(new Response(200, "Success: Link found", links));
        }

        // PUT: api/Links/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLinks(int id, Links links)
        {

            if (id != links.LinkId)
            {
                return BadRequest(new Response(400, "Error: Request Invalid/Corrupted. Try again later.", null));
            }

            _context.Entry(links).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LinksExists(id))
                {
                    return NotFound(new Response(404, "Error NotFound: link does not exist", null));
                }
                else
                {
                    throw;
                }
            }

            return Ok(new Response(200, "Success: Change was posted.", CreatedAtAction("GetLinks", new { id = links.LinkId }, links).Value));
        }

        // POST: api/Links
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Links>> PostLinks(Links links)
        {
            if (_context.Links == null)
            {
                return NotFound(new Response(404, "Error NotFound: Links do not exist", null));
            }
            _context.Links.Add(links);
            await _context.SaveChangesAsync();

            return StatusCode(201, new Response(201, "Success: Link was added." , CreatedAtAction("GetLinks", new { id = links.LinkId }, links)));
        }

        // DELETE: api/Links/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLinks(int id)
        {
            if (_context.Links == null)
            {
                return NotFound(new Response(404, "Error NotFound: Links do not exist", null));
            }
            var links = await _context.Links.FindAsync(id);
            if (links == null)
            {
                return NotFound(new Response(404, "Error NotFound: Links do not exist", null));
            }

            _context.Links.Remove(links);
            await _context.SaveChangesAsync();

            return Ok(new Response(200, "Success: Link was deleted.", ""));
        }

        private bool LinksExists(int id)
        {
            return _context.Links.Any(e => e.LinkId == id);
        }
    }
}
