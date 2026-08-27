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

        public HotelApiController(IHotelRepository hotelRepo)
        {
            _hotelRepo = hotelRepo;
        }

        [HttpGet("findHotel/{name}")]
        public async Task<IActionResult> FindHotel(string name)
        {
            var hotel = await _hotelRepo.Get(name);
            return hotel != null ? Ok(new HotelDto { Name = hotel.Name }) : NotFound();
        }
    }
}