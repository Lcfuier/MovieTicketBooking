using Microsoft.EntityFrameworkCore;
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
    public class SeatRepository : Repository<Seat>, ISeatRepository
    {
        public SeatRepository(MovieTicketBookingDbcontext context) : base(context) { }
        public void Update(Seat seat)
        {
            _context.Update(seat);
        }
    }
}
