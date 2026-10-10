using System;

namespace RestoApp.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int? UserId { get; set; }
        public int? TableId { get; set; }
        public string GuestName { get; set; }
        public string GuestEmail { get; set; }
        public string GuestPhone { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime? EndTime { get; set; }
        public int NumberOfGuests { get; set; }
        public string Status { get; set; } = "Pending";
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
