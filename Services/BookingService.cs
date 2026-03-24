using BnBApp.Models;

namespace BnBApp.Services
{
    public class BookingService
    {
        private List<Booking> bookings = new List<Booking>();

        // Creates a new booking and calculates total price
        public Booking CreateBooking(Room room, DateTime startDate, DateTime endDate)
        {
            // Check if room is available
            if (!CheckAvailability(room, startDate, endDate))
            {
                throw new Exception("Room is not available.");
            }

            // Calculate total price for the booking
            var totalPrice = CalculatePrice(room, startDate, endDate);

            // Create booking object
            var booking = new Booking
            {
                Room = room,
                StartDate = startDate,
                EndDate = endDate,
                TotalPrice = totalPrice
            };
            // Add booking to the list
            bookings.Add(booking);

            // Return the created booking
            return booking;
        }

        public decimal CalculatePrice(Room room, DateTime startDate, DateTime endDate)
        {
            int days = (endDate - startDate).Days;
            return room.PricePerDay * days;
        }

        // Checks if the room is available for booking
        public bool CheckAvailability(Room room, DateTime startDate, DateTime endDate)
        {
            foreach (var existingBooking in bookings)
            {
                if (existingBooking.Room.Id == room.Id)
                {
                    // Check overlap
                    if (startDate < existingBooking.EndDate && endDate > existingBooking.StartDate)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
