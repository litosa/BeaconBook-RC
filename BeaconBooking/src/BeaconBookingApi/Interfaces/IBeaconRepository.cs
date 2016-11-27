using BeaconBookingApi.Helpers;
using BeaconBookingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeaconBookingApi.Interfaces
{
    public interface IBeaconRepository
    {
        string GetRoomFromMinorId(int id);

        string GetZoneFromMajorId(int id);

        BeaconType GetBeaconType(int minorId, int majorId);

        Task<bool> SaveChangesAsync();
    }
}
