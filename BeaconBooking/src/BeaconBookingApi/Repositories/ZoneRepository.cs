using BeaconBookingApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeaconBookingApi.Models;
using BeaconBookingApi.Context;
using Microsoft.Extensions.Logging;

namespace BeaconBookingApi.Repositories
{
    public class ZoneRepository : IZoneRepository
    {
        private BeaconBookingContext _context;
        private ILogger<ZoneRepository> _logger;

        public ZoneRepository(BeaconBookingContext context, ILogger<ZoneRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Zone GetZoneById(int id)
        {
            return _context.Zones.FirstOrDefault(z => z.Id == id);
        }

        public IEnumerable<Zone> GetZones()
        {
            return _context.Zones.ToList();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
