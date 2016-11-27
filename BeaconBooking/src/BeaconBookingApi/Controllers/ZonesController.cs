using AutoMapper;
using BeaconBookingApi.Interfaces;
using BeaconBookingApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeaconBookingApi.Controllers
{
    [Route("/api/zones")]
    public class ZonesController : Controller
    {
        private IZoneRepository _zoneRepository;
        private IUserRepository _userRepository;
        private ILogger<ZonesController> _logger;

        public ZonesController(IZoneRepository zoneRepository, IUserRepository userRepository, ILogger<ZonesController> logger)
        {
            _zoneRepository = zoneRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var zoneViewModels = new List<ZoneViewModel>();

                foreach (var zone in _zoneRepository.GetZones())
                {
                    var userViewModels = Mapper.Map<IEnumerable<UserViewModel>>(_userRepository.GetAllUsers()
                        .Where(u => (u.CurrentZone == zone.Name && !u.IsHiding) 
                        || (zone.Name == "Dolda" && u.IsHiding ))).ToList();

                    zoneViewModels.Add(new ZoneViewModel
                    {
                        Id = zone.Id,
                        Name = zone.Name,
                        UserViewModels = userViewModels
                    });
                }

                return Ok(zoneViewModels);
            }

            catch (Exception exception)
            {
                _logger.LogError("Failed to get zones", exception);
            }

            return BadRequest("Failed to get zones");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]EditZoneViewModel model)
        {
            try
            {
                var zone = _zoneRepository.GetZoneById(id);

                if (zone != null)
                {
                    if (model.Id != null)
                    {
                        zone.Id = (int)model.Id;
                    }

                    if (!string.IsNullOrWhiteSpace(model.Name))
                    {
                        zone.Name = model.Name;
                    }

                    if (await _zoneRepository.SaveChangesAsync())
                    {
                        return Ok(zone);
                    }
                }
            }

            catch (Exception exception)
            {
                _logger.LogError("Failed to update zone", exception);
            }

            return BadRequest("Failed to update zone");
        }
    }
}
