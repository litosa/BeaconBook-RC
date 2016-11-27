using BeaconBookingApi.Interfaces;
using BeaconBookingApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BeaconBookingApi.Controllers
{
    [AllowAnonymous]
    [Route("/api/locations")]
    public class LocationsController : Controller
    {
        private ILogger<LocationsController> _logger;
        private ILocationRepository _repository;

        public LocationsController(ILocationRepository repository, ILogger<LocationsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpPut("")]
        public async Task<IActionResult> Put([FromBody]LocationViewModel model)
        {
            try
            {
                _repository.UpdateUserLocation(model.UserName, model.Room, model.Zone);

                await _repository.SaveChangesAsync();
                return Ok();
            }

            catch (Exception exception)
            {
                _logger.LogError("Could not update user's current position: {0}", exception);
                return BadRequest("Could not update user's current position");
            }
        }
    }
}
