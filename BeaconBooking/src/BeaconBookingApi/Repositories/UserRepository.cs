using BeaconBookingApi.Context;
using BeaconBookingApi.Interfaces;
using BeaconBookingApi.Models;
using BeaconBookingApi.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeaconBookingApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private BeaconBookingContext _context;
        private ILogger<UserRepository> _logger;
        private UserManager<User> _userManager;

        public UserRepository(BeaconBookingContext context, ILogger<UserRepository> logger, UserManager<User> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddUser(RegisterViewModel model)
        {
            var user = new User { UserName = model.Username, Email = model.Email, DepartmentId = model.DepartmentId };

            var result = await _userManager.CreateAsync(user, model.Password);

            return result;
        }

        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                return _context.Users.Include(u => u.Department).ToList();
            }

            catch (Exception exception)
            {
                _logger.LogError("Could not get users from the database", exception);
                return null;
            }
        }

        public User GetUserByUsername(string username)
        {
            try
            {
                return _context.Users.FirstOrDefault(u => u.UserName == username);
            }

            catch (Exception exception)
            {
                _logger.LogError("Could not get user from the database", exception);
                return null;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void UpdateCurrentRoom(string username, string room)
        {
            try
            {
                _context.Users.FirstOrDefault(u => u.UserName == username).CurrentRoom = room;
            }

            catch (Exception exception)
            {
                _logger.LogError("Could not update user's current room", exception);
            }
        }

        public void UpdateCurrentZone(string username, string zone)
        {
            try
            {
                _context.Users.FirstOrDefault(u => u.UserName == username).CurrentZone = zone;
            }

            catch (Exception exception)
            {
                _logger.LogError("Could not update user's current zone", exception);
            }
        }

        public PasswordVerificationResult CheckUserPassword(User user, string password)
        {
            var passwordHasher = new PasswordHasher<User>();

            return passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        }
    }
}
