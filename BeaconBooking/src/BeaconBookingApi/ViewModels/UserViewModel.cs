using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeaconBookingApi.ViewModels
{
    public class UserViewModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Department { get; set; }

        public string CurrentZone { get; set; }

        public string CurrentRoom { get; set; }

        public bool IsHiding { get; set; }

        public bool IsInBuilding { get; set; }
    }
}
