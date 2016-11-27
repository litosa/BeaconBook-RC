using BeaconBookingApi.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeaconBookingApi.Models;
using BeaconBookingApi.Context;
using Microsoft.EntityFrameworkCore;
using BeaconBookingApi.ViewModels;

namespace BeaconBookingApi.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private BeaconBookingContext _context;
        private ILogger<BookingRepository> _logger;

        public BookingRepository(BeaconBookingContext context, ILogger<BookingRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
        }

        public Booking GetActiveBookingFirstOrDefault(string userId, int roomId)
        {
            return _context.Bookings.FirstOrDefault(
                b => b.UserId == userId && b.RoomId == roomId && !b.IsCheckedIn
                && (DateTime.Now - b.DateTimeBooked).TotalMinutes < 15
                && (DateTime.Now - b.DateTimeBooked).TotalMinutes > -15);
        }

        public void CheckIn(Booking booking)
        {
            booking.IsCheckedIn = true;
        }

        public void RemoveBooking(int id)
        {
            var booking = _context.Bookings.Where(b => b.Id == id).FirstOrDefault();
            _context.Bookings.Remove(booking);
        }

        public IEnumerable<Booking> GetBookingsByRoomId(int id)
        {
            _logger.LogInformation("Getting all bookings from the database");

            IEnumerable<Booking> roomBookingList = _context.Bookings.Where(b => b.RoomId == id).Include(u => u.User).ToList();

            return roomBookingList;
        }

        public BookingViewModel GetBookingViewModel(Booking booking)
        {
            return new BookingViewModel
            {
                Username = booking.User.UserName,
                BookedFrom = booking.DateTimeBooked,
                BookedTo = booking.DateTimeBooked.AddMinutes(booking.BookingLengthMinutes),
                CheckedIn = booking.IsCheckedIn,
                RoomName = booking.Room.Name
            };
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public Booking GetBookingById(int id)
        {
            return _context.Bookings.FirstOrDefault(b => b.Id == id);
        }

        public bool HasActiveBooking(string userId, int roomId)
        {
            return _context.Bookings.Any(
                b => b.UserId == userId && b.RoomId == roomId && !b.IsCheckedIn
                && (DateTime.Now - b.DateTimeBooked).TotalMinutes < 15
                && (DateTime.Now - b.DateTimeBooked).TotalMinutes > -15);
        }
    }
}
