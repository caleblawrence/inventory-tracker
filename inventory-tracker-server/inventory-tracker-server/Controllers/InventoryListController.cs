using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

    }
}
