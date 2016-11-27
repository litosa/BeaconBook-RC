using BeaconBookingApi.Context;
using BeaconBookingApi.Interfaces;
using BeaconBookingApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeaconBookingApi.Helpers;

namespace BeaconBookingApi.Repositories
{
    public class BeaconRepository : IBeaconRepository
    {
        private BeaconBookingContext _context;
        private ILogger<BeaconRepository> _logger;

        public BeaconRepository(BeaconBookingContext context, ILogger<BeaconRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public BeaconType GetBeaconType(int minorId, int majorId)
        {
            if (_context.Rooms.Any(r => r.Id == minorId))
            {
                return BeaconType.Room;
            }

            else
            {
                return BeaconType.Zone;
            }
        }

        public string GetRoomFromMinorId(int id)
        {
            try
            {
                var room = _context.Rooms.FirstOrDefault(r => r.Id == id);

                return room != null ? room.Name : "Inget";
            }

            catch (Exception exception)
            {
                _logger.LogError("Could not get room from the database", exception);
                return null;
            }
        }

        public string GetZoneFromMajorId(int id)
        {
            try
            {
                var zone = _context.Zones.FirstOrDefault(b => b.Id == id);

                return zone != null ? zone.Name : "Ingen";
            }

            catch (Exception exception)
            {
                _logger.LogError("Could not get zone from the database", exception);
                return null;
            }
        }
       
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
