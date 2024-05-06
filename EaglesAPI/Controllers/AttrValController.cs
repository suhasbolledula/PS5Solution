using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eagles.EF.Data;
using Eagles.EF.Models;

namespace EaglesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttrValController : ControllerBase
    {
        private readonly EaglesOracleContext _context;

        public AttrValController(EaglesOracleContext context)
        {
            _context = context;
        }

        // GET: api/AttrVal
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttrVal>>> GetAttrVal()
        {
            return await _context.AttrVals.ToListAsync();
        }

        // GET: api/AttrVal/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AttrVal>> GetAttrVal(string id)
        {
            var attrval = await _context.AttrVals.FindAsync(id);

            if (attrval == null)
            {
                return NotFound();
            }

            return attrval;
        }

        // PUT: api/AttrVal/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttrVal(string id, AttrVal attrval)
        {
            if (id != attrval.AttrValId)
            {
                return BadRequest();
            }

            _context.Entry(attrval).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttrValExists(id))
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

        // POST: api/AttrVal
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AttrVal>> PostAttrVal(AttrVal attrval)
        {
            _context.AttrVals.Add(attrval);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttrVal", new { id = attrval.AttrValId }, attrval);
        }

        // DELETE: api/AttrVal/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttrVal(string id)
        {
            var attrval = await _context.AttrVals.FindAsync(id);
            if (attrval == null)
            {
                return NotFound();
            }

            _context.AttrVals.Remove(attrval);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AttrValExists(string id)
        {
            return _context.AttrVals.Any(e => e.AttrValId == id);
        }
    }
}