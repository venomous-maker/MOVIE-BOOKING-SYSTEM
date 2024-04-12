using eTickets.Data.Base;
using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class CinemasService:EntityBaseRepository<Cinema>, ICinemasService
    {
        private readonly AppDbContext _context;

        public CinemasService(AppDbContext context) : base(context)
        {
            _context = context;
        }
        
        public async Task<Cinema> GetCinemaByIdAsync(int id)
        {
            var cinemaDetails = await _context.Cinemas
                .Include(m => m.MovieCinemas)
                .ThenInclude(mov => mov.Movie)
                .FirstOrDefaultAsync(n => n.Id == id);
            return cinemaDetails;
        }
    }
}
