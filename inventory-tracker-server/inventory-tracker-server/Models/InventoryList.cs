using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace inventory_tracker_server.Models
{
    public class InventoryList
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string name { get; set; }
        public List<Item> Items { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
