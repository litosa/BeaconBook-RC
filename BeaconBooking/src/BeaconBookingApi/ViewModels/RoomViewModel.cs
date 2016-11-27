using BeaconBookingApi.Models;
using BeaconBookingApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaconBookingApi.ViewModels
{
    public class RoomViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public RoomStatus RoomStatus { get; set; }

        public ICollection<BookingViewModel> BookingViewModels { get; set; }
    }
}
