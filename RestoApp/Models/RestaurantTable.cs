using System;

namespace RestoApp.Models
{
    public class RestaurantTable
    {
        public int TableId { get; set; }
        public string TableNumber { get; set; }
        public int SeatingCapacity { get; set; }
        public string Location { get; set; }
        public string PhotoPath { get; set; }
        public bool IsActive { get; set; }
    }
}
