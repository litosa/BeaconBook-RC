using BeaconBookingApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeaconBookingApi.Context
{
    public class BeaconBookingContext : IdentityDbContext<User>
    {
        private IConfigurationRoot _config;

        public BeaconBookingContext(IConfigurationRoot config, DbContextOptions options)
            : base(options)
        {
            _config = config;
        }

        public BeaconBookingContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Beacon> Beacons { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Zone> Zones { get; set; }
    }
}
