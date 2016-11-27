using BeaconBookingApi.Models;
using BeaconBookingApi.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeaconBookingApi.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> AddUser(RegisterViewModel model);

        void UpdateCurrentRoom(string username, string room);

        void UpdateCurrentZone(string username, string zone);

        PasswordVerificationResult CheckUserPassword(User user, string password);

        IEnumerable<User> GetAllUsers();

        User GetUserByUsername(string username);

        Task<bool> SaveChangesAsync();
    }
}
