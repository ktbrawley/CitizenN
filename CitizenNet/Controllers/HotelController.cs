using CitizenNet.API.Repositories;
using CitizenNet.Core.Models;
using CitizenNet.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CitizenNet.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepo;
        private readonly IRoomRepository _roomRepo;

        public HotelController(IHotelRepository hotelRepo, IRoomRepository roomRepo)
        {
            _hotelRepo = hotelRepo;
            _roomRepo = roomRepo;
        }

        /// <summary>
        /// Finds a hotel by its exact name.
        /// </summary>
        /// <param name="name">The hotel name to look up.</param>
        [HttpGet("find-hotel/{name}")]
        [ProducesResponseType(typeof(HotelDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FindHotel(string name)
        {
            var hotel = await _hotelRepo.Get(name);
            return hotel != null ? Ok(new HotelDto { Name = hotel.Name }) : NotFound();
        }

        /// <summary>
        /// Finds rooms available at a named hotel for a given stay and guest count.
        /// </summary>
        /// <param name="request">The hotel name, stay dates, and guest count to search for.</param>
        /// <response code="200">Rooms matching the search criteria (may be empty).</response>
        /// <response code="400">The request was missing a hotel name or had an invalid date range.</response>
        /// <response code="404">No hotel with the given name was found.</response>
        [HttpPost("room-search")]
        [ProducesResponseType(typeof(IReadOnlyList<RoomDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
            var roomDtos = availableRooms.Select(r => new RoomDto
            {
                Id = r.Id,
                RoomNumber = r.RoomNumber,
                RoomType = r.RoomType.ToString(),
                Capacity = r.Capacity,
            }).ToList();
            return Ok(roomDtos);
        }
    }
}