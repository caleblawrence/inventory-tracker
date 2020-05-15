using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using inventory_tracker_server.Data;
using inventory_tracker_server.Models;

namespace inventory_tracker_server.Controllers
{
    [ApiController]
    public class InventoryListController : Controller
    {
        private readonly ApplicationDbContext _db;

        public InventoryListController(ApplicationDbContext context)
        {
            _db = context;
        }

        [HttpGet("InventoryList/all")]
        public async Task<IEnumerable<InventoryList>> GetAll()
        {
            var lists = await _db.InventoryList.ToListAsync();
            return lists;
        }

        // GET: InventoryList/Details/5
    }
}
