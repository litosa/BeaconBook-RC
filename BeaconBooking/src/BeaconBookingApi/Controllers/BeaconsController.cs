using BeaconBookingApi.Interfaces;
using BeaconBookingApi.Models;
using BeaconBookingApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeaconBookingApi.Controllers
{
    [AllowAnonymous]
    [Route("/api/beacons")]
    public class BeaconsController : Controller
    {
        private IBeaconRepository _beaconRepository;
        private IUserRepository _userRepository;
        private ILogger<BeaconsController> _logger;
        private IBookingRepository _bookingRepository;

        public BeaconsController(IBeaconRepository beaconRepository, IUserRepository userRepository, IBookingRepository bookingRepository, ILogger<BeaconsController> logger)
        {
            _beaconRepository = beaconRepository;
            _userRepository = userRepository;
            _bookingRepository = bookingRepository;
            _logger = logger;
        }

        [HttpPost("")]
        public IActionResult Post([FromBody]BeaconViewModel model)
        {
            try
            {
                var result = new BeaconResult
                {
                    Room = _beaconRepository.GetRoomFromMinorId(model.MinorId),
                    Zone = _beaconRepository.GetZoneFromMajorId(model.MajorId),
                    BeaconType = _beaconRepository.GetBeaconType(model.MinorId, model.MajorId),
                    HasActiveBooking = _bookingRepository.HasActiveBooking(model.UserName, model.MinorId)
                };

                return Ok(result);
            }

            catch (Exception exception)
            {
                _logger.LogError("Could not get beacon result: {0}", exception);
                return BadRequest("Could not get beacon result");
            }
        }
    }
}
