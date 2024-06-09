using MovieTicketBooking.Domain.Interfaces;
using MovieTicketBooking.Domain.Models;
using MovieTicketBooking.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Infrastructure.Repository
{
    public class MovieRepository : Repository<Movie> , IMovieRepository
    {
        public MovieRepository(MovieTicketBookingDbcontext context) : base(context) { }
        public void Update(Movie movie)
        {
            _context.Update(movie);
        }
    }

}
