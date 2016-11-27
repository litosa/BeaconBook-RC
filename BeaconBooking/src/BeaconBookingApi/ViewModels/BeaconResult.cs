using BeaconBookingApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeaconBookingApi.ViewModels
{
    public class BeaconResult
    {
        public string Room { get; set; }

        public string Zone { get; set; }

        public BeaconType BeaconType { get; set; }

        public bool HasActiveBooking { get; set; }
    }
}
