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
    public class CinemaRepository : Repository<Cinema>,ICinemaRepository
    {
        public CinemaRepository(MovieTicketBookingDbcontext context) : base(context) { }
        public void Update(Cinema cinema)
        {
            _context.Update(cinema);
        }
    }
}
