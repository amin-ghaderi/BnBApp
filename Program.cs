using BnBApp.Models;
using BnBApp.Services;

// Create a sample room
var rooms = new List<Room>
{
    new Room { Id = 1, Name = "Single Room (11m², 1 bed)", Capacity = 1, PricePerDay = 550 },
    new Room { Id = 2, Name = "Double Room (14m², 2 beds)", Capacity = 2, PricePerDay = 700 },
    new Room { Id = 3, Name = "Double Room (16m², 1 double bed)", Capacity = 2, PricePerDay = 765 },
    new Room { Id = 4, Name = "Family Room (24m², 3 beds)", Capacity = 3, PricePerDay = 850 }
};

// Initialize booking service
var bookingService = new BookingService();

// Define booking dates
var startDate = new DateTime(2026, 4, 1);
var endDate = new DateTime(2026, 4, 5);

//var booking = bookingService.CreateBooking(room, startDate, endDate);
var booking = bookingService.CreateBooking(rooms[0], startDate, endDate);

Console.WriteLine($"Room: {booking.Room.Name}");
Console.WriteLine($"From: {booking.StartDate}");
Console.WriteLine($"To: {booking.EndDate}");
Console.WriteLine($"Total Price: {booking.TotalPrice}");



Console.WriteLine($"\nTrying second booking...");

try
{
    var booking2 = bookingService.CreateBooking(rooms[0], startDate, endDate);
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

Console.WriteLine("\nAvailable rooms:");

foreach (var r in rooms)
{
    if (bookingService.CheckAvailability(r, startDate, endDate))
        Console.WriteLine($"{r.Name} is available");
    else
        Console.WriteLine($"{r.Name} is NOT available");
}