using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeaconBookingApi.Models
{
    public class Room
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int ZoneId { get; set; }
        public Zone Zone { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
