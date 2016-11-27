using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BeaconBookingApi.Interfaces;
using BeaconBookingApi.Models;
using BeaconBookingApi.ViewModels;
using AutoMapper;
using BeaconBookingApi.Repositories;

namespace BeaconBookingApi.Controllers
{
    [Route("api/bookings")]
    public class BookingsController : Controller
    {
        private ILogger<BookingsController> _logger;
        private IBookingRepository _bRepository;
        private IRoomRepository _rRepository;

        public BookingsController(IBookingRepository bRepository, IRoomRepository rRepository, ILogger<BookingsController> logger)
        {
            _bRepository = bRepository;
            _rRepository = rRepository;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public IActionResult GetBookingById(int id)
        {
            try
            {
                var booking = _bRepository.GetBookingById(id);
                var bookingViewModel = _bRepository.GetBookingViewModel(booking);

                return Ok(bookingViewModel);
            }

            catch (Exception exception)
            {
                _logger.LogError("Failed to get booking", exception);
            }

            return BadRequest("Failed to get booking");

        }

        [HttpPost]
        public async Task<IActionResult> AddBooking([FromBody]AddBookingViewModel newBooking)
        {
            if (ModelState.IsValid)
            {
                var room = _rRepository.GetRoomById(newBooking.RoomId);
                var result = _rRepository.GetRoomStatus(
                    room, 
                    newBooking.DateTimeBooked, 
                    newBooking.DateTimeBooked.AddMinutes(newBooking.BookingLengthMinutes));

                if (result != RoomStatus.Booked)
                {
                    var booking = Mapper.Map<Booking>(newBooking);

                    _bRepository.AddBooking(booking);

                    if (await _bRepository.SaveChangesAsync())
                    {
                        return Created($"api/bookings", Mapper.Map<AddBookingViewModel>(newBooking));
                    }
                }
            }

            return BadRequest("Failed to save Booking");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            try
            {
                _bRepository.RemoveBooking(id);

                if (await _bRepository.SaveChangesAsync())
                {
                    return Ok();
                }
            }

            catch (Exception exception)
            {
                _logger.LogError("Failed to delete booking", exception);
            }

            return BadRequest("Failed to delete booking");
        }
    }
}

