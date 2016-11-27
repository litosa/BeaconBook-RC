using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;

namespace BeaconBookingApi.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsHiding { get; set; }

        public bool IsInBuilding { get; set; }

        public string CurrentZone { get; set; } = "Hemma";

        public string CurrentRoom { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }

        public ICollection<Booking> Bookings { get; set; }
    }
}
