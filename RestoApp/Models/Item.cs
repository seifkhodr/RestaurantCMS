using System;

namespace RestoApp.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public int MenuId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string Ingredients { get; set; }
        public string Origin { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }

        // Additional helpful properties
        public string PrimaryPhotoPath { get; set; } // Can be joined from ItemPhotos table
        public string MenuName { get; set; } // Can be joined from Menus table
    }
}
