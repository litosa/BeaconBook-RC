using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeaconBookingApi.ViewModels
{
    public class BookingViewModel
    {
        public string Username { get; set; }

        public DateTime BookedFrom { get; set; }

        public DateTime BookedTo { get; set; }

        public bool CheckedIn { get; set; }

        public string RoomName { get; set; }
    }
}
