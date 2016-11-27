using BeaconBookingApi.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaconBookingApi.Context
{
    public class BeaconContextSeedData
    {
        private BeaconBookingContext _context;
        private UserManager<User> _userManager;

        public BeaconContextSeedData(BeaconBookingContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task EnsureSeedData()
        {
            if (!_context.Departments.Any())
            {
                _context.Departments.Add(new Department
                {
                    Name = "Utvecklare"
                });

                _context.Departments.Add(new Department
                {
                    Name = "Designer"
                });

                _context.SaveChanges();
            }

            if (!_context.Users.Any())
            {
                var departmentId = _context.Departments.FirstOrDefault(d => d.Name == "Utvecklare").Id;

                await _userManager.CreateAsync(new User { UserName = "litos", Email = "litos@gmail.com", DepartmentId = departmentId }, "P@ssw0rd!");
                await _userManager.CreateAsync(new User { UserName = "neymar", Email = "calle@hotmail.com", DepartmentId = departmentId }, "P@ssw0rd!");
            }

            if (!_context.Zones.Any())
            {
                _context.Zones.Add(new Zone { Id = 51863, Name = "A" });
                _context.Zones.Add(new Zone { Id = 2, Name = "B" });
                _context.Zones.Add(new Zone { Id = 3, Name = "C" });
                _context.Zones.Add(new Zone { Id = 4, Name = "D" });
                _context.Zones.Add(new Zone { Id = 5, Name = "Hemma" });
                _context.Zones.Add(new Zone { Id = 6, Name = "Dolda" });
            }

            if (!_context.Rooms.Any())
            {
                _context.Rooms.Add(new Room { Id = 63995, Name = "Röda", ZoneId = 51863 });
                _context.Rooms.Add(new Room { Id = 2, Name = "Blåa", ZoneId = 2 });
                _context.Rooms.Add(new Room { Id = 3, Name = "Gröna", ZoneId = 2 });
                _context.Rooms.Add(new Room { Id = 4, Name = "Brandgula", ZoneId = 2 });
                _context.Rooms.Add(new Room { Id = 5, Name = "Rosa", ZoneId = 3 });
                _context.Rooms.Add(new Room { Id = 6, Name = "Lila", ZoneId = 3 });
            }

            await _context.SaveChangesAsync();
        }
    }
}

