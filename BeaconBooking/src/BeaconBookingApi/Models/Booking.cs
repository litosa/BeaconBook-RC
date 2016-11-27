using System;

namespace BeaconBookingApi.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public DateTime DateTimeBooked { get; set; }

        public int BookingLengthMinutes { get; set; }

        public bool IsCheckedIn { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
