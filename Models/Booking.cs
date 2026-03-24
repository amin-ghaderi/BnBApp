using System;

namespace BnBApp.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public Room Room { get; set; } = new Room();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
