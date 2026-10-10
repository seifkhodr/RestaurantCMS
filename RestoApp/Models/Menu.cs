using System;
using System.Collections.Generic;

namespace RestoApp.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }


        public List<Item> Items { get; set; } = new List<Item>();
    }
}
