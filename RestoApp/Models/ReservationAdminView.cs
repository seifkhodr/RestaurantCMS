using System;

namespace RestoApp.Models
{
    public class ReservationAdminView : Reservation
    {
        public string TableNumber { get; set; }
        public int SeatingCapacity { get; set; }
    }
}
