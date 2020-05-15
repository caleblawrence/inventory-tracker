using System;
using System.ComponentModel.DataAnnotations;

namespace inventory_tracker_server.Models
{
    public class Item
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string name { get; set; }
        public int InventoryListId { get; set; }
        public InventoryList InventoryList { get; set; }
    }

}