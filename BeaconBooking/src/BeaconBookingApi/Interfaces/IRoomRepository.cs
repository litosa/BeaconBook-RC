using BeaconBookingApi.Models;
using BeaconBookingApi.Repositories;
using BeaconBookingApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaconBookingApi.Interfaces
{
    public interface IRoomRepository
    {
        Room GetRoomById(int id);

        IEnumerable<Room> GetAllRooms();

        RoomViewModel GetRoomViewModel(Room room);

        RoomStatus GetRoomStatus(Room room, DateTime fromDateTime, DateTime toDateTime);

        Task<bool> SaveChangesAsync();

    }
}
