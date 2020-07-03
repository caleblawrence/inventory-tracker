using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace inventory_tracker_server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<InventoryList> InventoryList { get; set; }
    }
}
