using System;
using System.Collections.Generic;
using System.Text;
using CitizenNet.Data;
using CitizenNet.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CitizenNet.API.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelDbContext _context;

        public HotelRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<Hotel> Get(int index)
        {
            return await _context?.Hotels?.Where(h => h.Id == index)?.FirstOrDefaultAsync();
        }

        public async Task<Hotel> Get(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            return await _context?.Hotels?.Where(h => h.Name.ToLower() == name.ToLower())?.FirstOrDefaultAsync();
        }
    }
}