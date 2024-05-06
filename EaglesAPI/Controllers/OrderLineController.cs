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
    public class OrderLinesController : ControllerBase
    {
        private readonly EaglesOracleContext _context;

        public OrderLinesController(EaglesOracleContext context)
        {
            _context = context;
        }

        // GET: api/OrderLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdersLine>>> GetOrderLines()
        {
            return await _context.OrdersLines.ToListAsync();
        }

        // GET: api/OrderLines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdersLine>> GetOrderLine(string id)
        {
            var orderLine = await _context.OrdersLines.FindAsync(id);

            if (orderLine == null)
            {
                return NotFound();
            }

            return orderLine;
        }

        // PUT: api/OrderLines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderLine(string id, OrdersLine orderLine)
        {
            if (id != orderLine.OrdersLineId)
            {
                return BadRequest();
            }

            _context.Entry(orderLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderLineExists(id))
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

        // POST: api/OrderLines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrdersLine>> PostOrderLine(OrdersLine orderLine)
        {
            _context.OrdersLines.Add(orderLine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderLine", new { id = orderLine.OrdersLineId }, orderLine);
        }

        // DELETE: api/OrderLines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderLine(string id)
        {
            var orderLine = await _context.OrdersLines.FindAsync(id);
            if (orderLine == null)
            {
                return NotFound();
            }

            _context.OrdersLines.Remove(orderLine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderLineExists(string id)
        {
            return _context.OrdersLines.Any(e => e.OrdersLineId == id);
        }
    }
}