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
    public class BookingRepository : Repository<Booking>,IBookingRepository
    {
        public BookingRepository(MovieTicketBookingDbcontext context) : base(context) { }
        public void Update(Booking booking)
        {
            _context.Update(booking);
        }
    }
}
