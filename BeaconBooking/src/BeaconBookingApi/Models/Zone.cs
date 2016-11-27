using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeaconBookingApi.Models
{
    public class Zone
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Room> Rooms { get; set; }
    }
}