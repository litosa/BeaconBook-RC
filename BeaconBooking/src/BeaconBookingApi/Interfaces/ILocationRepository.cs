using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeaconBookingApi.Interfaces
{
    public interface ILocationRepository
    {
        void UpdateUserLocation(string userName, string room, string zone);

        Task<bool> SaveChangesAsync();
    }
}
