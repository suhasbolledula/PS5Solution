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
    public class ProductAttrController : ControllerBase
    {
        private readonly EaglesOracleContext _context;

        public ProductAttrController(EaglesOracleContext context)
        {
            _context = context;
        }

        // GET: api/ProductAttr
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductAttr>>> GetProductAttr()
        {
            return await _context.ProductAttrs.ToListAsync();
        }

        // GET: api/ProductAttr/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductAttr>> GetProductAttr(string id)
        {
            var productattr = await _context.ProductAttrs.FindAsync(id);

            if (productattr == null)
            {
                return NotFound();
            }

            return productattr;
        }

        // PUT: api/ProductAttr/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductAttr(string id, ProductAttr productattr)
        {
            if (id != productattr.ProductAttrId)
            {
                return BadRequest();
            }

            _context.Entry(productattr).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductAttrExists(id))
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

        // POST: api/ProductAttr
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductAttr>> PostProductAttr(ProductAttr productattr)
        {
            _context.ProductAttrs.Add(productattr);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductAttr", new { id = productattr.ProductAttrId }, productattr);
        }

        // DELETE: api/ProductAttr/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAttr(string id)
        {
            var productattr = await _context.ProductAttrs.FindAsync(id);
            if (productattr == null)
            {
                return NotFound();
            }

            _context.ProductAttrs.Remove(productattr);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductAttrExists(string id)
        {
            return _context.ProductAttrs.Any(e => e.ProductAttrId == id);
        }
    }
}