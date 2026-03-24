using BnBApp.Models;
using BnBApp.Services;

/// <summary>
/// Entry point of the Bed & Breakfast console application.
/// Handles user interaction and menu navigation.
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        // Initialize available rooms in the system
        var rooms = new List<Room>
        {new Room { Id = 1, Name = "Single Room (11m², 1 single bed, flat-screen TV)", Capacity = 1, PricePerDay = 550 },
        new Room { Id = 2, Name = "Double Room (14m², 2 single beds, flat-screen TV)", Capacity = 2, PricePerDay = 700 },
        new Room { Id = 3, Name = "Double Room (16m², 1 double bed, flat-screen TV)", Capacity = 2, PricePerDay = 765 },
        new Room { Id = 4, Name = "Family Room (24m², 3 single beds, flat-screen TV)", Capacity = 3, PricePerDay = 850 }
        };

        // Service responsible for booking logic
        var bookingService = new BookingService();

        // Main application loop
        while (true)
        {
            DisplayMenu();

            Console.Write("Choose option: ");
            string choice = Console.ReadLine()!;

            switch (choice)
            {
                case "1":
                    ShowRooms(rooms);
                    break;

                case "2":
                    CreateBooking(rooms, bookingService);
                    break;

                case "3":
                    CheckAvailability(rooms, bookingService);
                    break;

                case "4":
                    Console.WriteLine("Exiting application...");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    /// <summary>
    /// Displays the main menu options.
    /// </summary>
    static void DisplayMenu()
    {
        Console.WriteLine("\n=== HOTEL MENU ===");
        Console.WriteLine("1. Show Rooms");
        Console.WriteLine("2. Create Booking");
        Console.WriteLine("3. Check Availability");
        Console.WriteLine("4. Exit");
    }

    /// <summary>
    /// Displays all available rooms.
    /// </summary>
    /// <param name="rooms">List of rooms</param>
    static void ShowRooms(List<Room> rooms)
    {
        Console.WriteLine("\nRooms:");

        foreach (var room in rooms)
        {
            Console.WriteLine($"{room.Id}. {room.Name} - {room.PricePerDay} SEK/night");
        }
    }

    /// <summary>
    /// Handles the booking creation process.
    /// </summary>
    /// <param name="rooms">List of rooms</param>
    /// <param name="bookingService">Booking service instance</param>
    static void CreateBooking(List<Room> rooms, BookingService bookingService)
    {
        Console.WriteLine("\nSelect a room:");

        foreach (var room in rooms)
        {
            Console.WriteLine($"{room.Id}. {room.Name}");
        }

        Console.Write("Enter Room Id: ");
        int roomId = int.Parse(Console.ReadLine()!);

        Room? selectedRoom = null;

        // Find room
        foreach (var room in rooms)
        {
            if (room.Id == roomId)
            {
                selectedRoom = room;
                break;
            }
        }

        // Validate room selection
        if (selectedRoom == null)
        {
            Console.WriteLine("Room not found!");
            return;
        }

        Console.Write("Start date (yyyy-mm-dd): ");
        DateTime startDate = DateTime.Parse(Console.ReadLine()!);

        Console.Write("End date (yyyy-mm-dd): ");
        DateTime endDate = DateTime.Parse(Console.ReadLine()!);

        try
        {
            // Create booking using service layer
            var booking = bookingService.CreateBooking(selectedRoom, startDate, endDate);

            Console.WriteLine("\nBooking created successfully!");
            Console.WriteLine($"Room: {booking.Room.Name}");
            Console.WriteLine($"Total price: {booking.TotalPrice} DKK");
        }
        catch (Exception ex)
        {
            // Handle booking errors (e.g., room not available)
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Checks availability of all rooms for a given date range.
    /// </summary>
    /// <param name="rooms">List of rooms</param>
    /// <param name="bookingService">Booking service instance</param>
    static void CheckAvailability(List<Room> rooms, BookingService bookingService)
    {
        Console.Write("Start date (yyyy-mm-dd): ");
        DateTime startDate = DateTime.Parse(Console.ReadLine()!);

        Console.Write("End date (yyyy-mm-dd): ");
        DateTime endDate = DateTime.Parse(Console.ReadLine()!);

        Console.WriteLine("\nAvailable rooms:");

        foreach (var room in rooms)
        {
            bool isAvailable = bookingService.CheckAvailability(room, startDate, endDate);

            if (isAvailable)
                Console.WriteLine($"{room.Name} is available");
            else
                Console.WriteLine($"{room.Name} is NOT available");
        }
    }
}