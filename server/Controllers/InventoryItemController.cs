using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using inventory_tracker_server.Data;
using inventory_tracker_server.Models;
using Microsoft.AspNetCore.Authorization;

namespace inventory_tracker_server.Controllerss
{
    [Authorize]
    [ApiController]
    // [Route("api/[controller]")]
    public class InventoryItemController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public InventoryItemController(ApplicationDbContext context)
        {
            _db = context;
        }

        [HttpGet("InventoryItem")]
        public async Task<IEnumerable<Item>> Get()
        {
            var items = await _db.Item.ToListAsync();
            return items;
        }


        [HttpPost("InventoryItem")]
        public async Task<ActionResult<Item>> Post(Item item)
        {

            _db.Item.Add(item);
            await _db.SaveChangesAsync();
            return Accepted();
        }

        [HttpGet("InventoryItem/{id}")]
        public async Task<ActionResult<Item>> Get(int id)
        {
            var item = await _db.Item.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPut("InventoryItem/{id}")]
        public async Task<IActionResult> Put(int id, Item item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _db.Entry(item).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryItemExists(id))
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

        [HttpDelete("InventoryItem/{id}")]
        public async Task<ActionResult<Item>> delete(int id)
        {
            var item = await _db.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _db.Item.Remove(item);
            await _db.SaveChangesAsync();

            return item;
        }

        private bool InventoryItemExists(long id) =>
         _db.Item.Any(e => e.Id == id);
    }
}
