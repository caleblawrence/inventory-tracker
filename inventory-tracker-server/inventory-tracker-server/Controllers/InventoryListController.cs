using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using inventory_tracker_server.Data;
using inventory_tracker_server.Models;
using Microsoft.AspNetCore.Authorization;

namespace inventory_tracker_server.Controllers
{
    [Authorize]
    [ApiController]
    public class InventoryListController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public InventoryListController(ApplicationDbContext context)
        {
            _db = context;
        }

        [HttpGet("InventoryList")]
        public async Task<IEnumerable<InventoryList>> Get()
        {
            var lists = await _db.InventoryList.ToListAsync();
            return lists;
        }


        [HttpPost("InventoryList")]
        public async Task<ActionResult<InventoryList>> Post(InventoryList inventoryList)
        {
            var userId = _db.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault().Id;

            inventoryList.UserId = userId;

            _db.InventoryList.Add(inventoryList);
            await _db.SaveChangesAsync();
            return Accepted();
        }

        [HttpGet("InventoryList/{id}")]
        public async Task<ActionResult<InventoryList>> Get(int id)
        {
            var inventoryList = await _db.InventoryList.FindAsync(id);

            if (inventoryList == null)
            {
                return NotFound();
            }

            return inventoryList;
        }

        [HttpPut("InventoryList/{id}")]
        public async Task<IActionResult> Put(int id, InventoryList inventoryList)
        {
            if (id != inventoryList.Id)
            {
                return BadRequest();
            }

            _db.Entry(inventoryList).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryListExists(id))
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

        // DELETE: api/TodoItems/5
        [HttpDelete("InventoryList/{id}")]
        public async Task<ActionResult<InventoryList>> DeleteTodoItem(int id)
        {
            var inventoryList = await _db.InventoryList.FindAsync(id);
            if (inventoryList == null)
            {
                return NotFound();
            }

            _db.InventoryList.Remove(inventoryList);
            await _db.SaveChangesAsync();

            return inventoryList;
        }

        private bool InventoryListExists(long id) =>
         _db.InventoryList.Any(e => e.Id == id);
    }
}
