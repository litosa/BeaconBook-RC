using BeaconBookingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeaconBookingApi.Interfaces
{
    public interface IZoneRepository
    {
        IEnumerable<Zone> GetZones();

        Zone GetZoneById(int id);

        Task<bool> SaveChangesAsync();
    }
}
