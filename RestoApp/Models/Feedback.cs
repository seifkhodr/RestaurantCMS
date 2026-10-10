using System;

namespace RestoApp.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        public int? UserId { get; set; }
        public int? ReservationId { get; set; }
        public string GuestName { get; set; }
        public string Comment { get; set; }
        public int VisitRating { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsApproved { get; set; }
    }
}
