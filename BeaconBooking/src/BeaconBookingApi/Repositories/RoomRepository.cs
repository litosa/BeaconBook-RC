using BeaconBookingApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeaconBookingApi.Models;
using BeaconBookingApi.Context;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using BeaconBookingApi.ViewModels;

namespace BeaconBookingApi.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private BeaconBookingContext _context;
        private ILogger<RoomRepository> _logger;

        public RoomRepository(BeaconBookingContext context, ILogger<RoomRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Room> GetAllRooms()
        {
            _logger.LogInformation("Getting all trips from the database");

            return _context.Rooms.Include(r => r.Bookings).ToList();
        }

        public Room GetRoomById(int id)
        {
            _logger.LogInformation("Getting all bookings from the database");

            Room room = _context.Rooms.Where(b => b.Id == id).Include(b => b.Bookings).FirstOrDefault();

            return room;
        }

        public RoomViewModel GetRoomViewModel(Room room)
        {
            return new RoomViewModel
            {
                Name = room.Name,
                Id = room.Id,
                RoomStatus = GetRoomStatus(room, DateTime.Now, DateTime.Now)
            };
        }

        public RoomStatus GetRoomStatus(Room room, DateTime fromDateTime, DateTime toDateTime)
        {
            var currentBooking = room.Bookings.FirstOrDefault(
                        b => fromDateTime < b.DateTimeBooked.AddMinutes(b.BookingLengthMinutes) && 
                        toDateTime > b.DateTimeBooked);

            if (currentBooking != null)
            {
                return !currentBooking.IsCheckedIn && (DateTime.Now - currentBooking.DateTimeBooked).TotalMinutes > 15 ? RoomStatus.BookedButNotCheckedIn : RoomStatus.Booked;
            }

            else
            {
                return RoomStatus.Available;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }

    public enum RoomStatus
    {
        Available,
        Booked,
        BookedButNotCheckedIn
    }
}
