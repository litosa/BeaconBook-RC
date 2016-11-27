using BeaconBookingApi.ViewModels;
using BeaconBookingApi.Interfaces;
using BeaconBookingApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BeaconBookingApi.Controllers
{
    [Route("/api/users")]
    public class UsersController : Controller
    {
        private IUserRepository _repository;
        private ILogger<UsersController> _logger;

        public UsersController(IUserRepository repository, ILogger<UsersController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var users = _repository.GetAllUsers();

                return Ok(Mapper.Map<IEnumerable<UserViewModel>>(users));
            }

            catch (Exception exception)
            {
                _logger.LogError("Failed to get users", exception);
            }

            return BadRequest("Failed to get users");
        }

        [HttpGet("{userName}")]
        public IActionResult Get(string userName)
        {
            try
            {
                var user = _repository.GetUserByUsername(userName);

                return Ok(Mapper.Map<UserViewModel>(user));
            }

            catch (Exception exception)
            {
                _logger.LogError("Failed to get user", exception);
            }

            return BadRequest("Failed to get user");
        }

        [HttpPut("{userName}")]
        public async Task<IActionResult> Put(string userName, [FromBody]UserViewModel model)
        {
            try
            {
                var user = _repository.GetUserByUsername(userName);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.IsHiding = model.IsHiding;

                await _repository.SaveChangesAsync();

                return Ok();
            }

            catch (Exception exception)
            {
                _logger.LogError("Failed to update user: {0}", exception);
            }

            return BadRequest("Failed to update user");
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _repository.AddUser(model);

                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                }
            }

            catch (Exception exception)
            {
                _logger.LogError("Failed to save new user: {0}", exception);
            }

            return BadRequest("Failed to save new user");
        }
    }
}
