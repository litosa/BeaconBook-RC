using BeaconBookingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeaconBookingApi.ViewModels;

namespace BeaconBookingApi.Interfaces
{
    public interface IBookingRepository
    {
        Booking GetBookingById(int id);

        IEnumerable<Booking> GetBookingsByRoomId(int id);        

        void AddBooking(Booking booking);

        void RemoveBooking(int id);

        bool HasActiveBooking(string userId, int roomId);

        Booking GetActiveBookingFirstOrDefault(string userId, int roomId);

        void CheckIn(Booking booking);

        Task<bool> SaveChangesAsync();
        BookingViewModel GetBookingViewModel(Booking booking);
    }
}
