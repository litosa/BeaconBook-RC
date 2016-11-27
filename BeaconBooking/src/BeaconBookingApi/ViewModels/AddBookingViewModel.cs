using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaconBookingApi.ViewModels
{
    public class AddBookingViewModel
    {
        public DateTime DateTimeBooked { get; set; }

        public int BookingLengthMinutes { get; set; }

        public int RoomId { get; set; }
    }
}
