using BeaconBookingApi.Context;
using BeaconBookingApi.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeaconBookingApi.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private BeaconBookingContext _context;
        private ILogger<LocationRepository> _logger;

        public LocationRepository(BeaconBookingContext context, ILogger<LocationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void UpdateUserLocation(string userName, string room, string zone)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == userName);

                if (user != null)
                {
                    user.CurrentRoom = room;
                    user.CurrentZone = zone;
                }
            }

            catch (Exception exception)
            {
                _logger.LogError("Could not update user's current room", exception);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
