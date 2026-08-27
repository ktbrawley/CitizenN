using System.Reflection.Metadata.Ecma335;
using CitizenNet.API.Repositories;
using CitizenNet.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CitizenNet.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class HotelApiController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepo;
        private readonly IRoomRepository _roomRepo;

        public HotelApiController(IHotelRepository hotelRepo, IRoomRepository roomRepo)
        {
            _hotelRepo = hotelRepo;
            _roomRepo = roomRepo;
        }

        [HttpGet("findHotel/{name}")]
        public async Task<IActionResult> FindHotel(string name)
        {
            var hotel = await _hotelRepo.Get(name);
            return hotel != null ? Ok(new HotelDto { Name = hotel.Name }) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> SearchForRoom([FromBody] RoomSearchRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.HotelName))
            {
                return BadRequest("Invalid request data.");
            }
            var hotel = await _hotelRepo.Get(request.HotelName);
            if (hotel == null)
            {
                return NotFound($"Hotel '{request.HotelName}' not found.");
            }
            var availableRooms = await _roomRepo.FindAvailableAsync(hotel.Id, request.CheckIn, request.CheckOut, request.GuestCount);
            return Ok(availableRooms);
        }
    }
}