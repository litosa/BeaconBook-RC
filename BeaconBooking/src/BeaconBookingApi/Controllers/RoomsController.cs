using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeaconBookingApi.Interfaces;
using Microsoft.Extensions.Logging;
using BeaconBookingApi.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BeaconBookingApi.Controllers
{
    [Route("api/rooms")]
    public class RoomsController : Controller
    {
        private ILogger<RoomsController> _logger;
        private IRoomRepository _rRepository;
        private IBookingRepository _bRepository;

        public RoomsController(IRoomRepository rRepository, IBookingRepository bRepository, ILogger<RoomsController> logger)
        {
            _rRepository = rRepository;
            _bRepository = bRepository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllRooms()
        {
            try
            {
                var roomViewModels = new List<RoomViewModel>();

                foreach (var room in _rRepository.GetAllRooms())
                {
                    var bookingViewModels = new List<BookingViewModel>();

                    foreach (var booking in _bRepository.GetBookingsByRoomId(room.Id))
                    {
                        bookingViewModels.Add(_bRepository.GetBookingViewModel(booking));
                    }

                    var roomViewModel = _rRepository.GetRoomViewModel(room);
                    roomViewModel.BookingViewModels = bookingViewModels;

                    roomViewModels.Add(roomViewModel);
                }

                return Ok(roomViewModels);
            }

            catch (Exception exception)
            {
                _logger.LogError("Failed to get rooms", exception);
            }

            return BadRequest("Failed to get rooms");
        }

        [HttpGet("{id}")]
        public IActionResult GetRoomById(int id)
        {
            try
            {
                var room = _rRepository.GetRoomById(id);
                var roomViewModel = _rRepository.GetRoomViewModel(room);

                return Ok(room);
            }

            catch (Exception exception)
            {
                _logger.LogError("Failed to get room", exception);
            }

            return BadRequest("Failed to get room");

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]EditRoomViewModel model)
        {
            try
            {
                var room = _rRepository.GetRoomById(id);

                if (room != null)
                {
                    if (model.Id != null)
                    {
                        room.Id = (int)model.Id;
                    }

                    if (!string.IsNullOrWhiteSpace(model.Name))
                    {
                        room.Name = model.Name;
                    }

                    if (await _rRepository.SaveChangesAsync())
                    {
                        return Ok(room);
                    }
                }
            }

            catch (Exception exception)
            {
                _logger.LogError("Failed to update room", exception);
            }

            return BadRequest("Failed to update room");
        }
    }
}