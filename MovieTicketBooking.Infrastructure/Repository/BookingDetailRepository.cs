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
    public class BookingDetailRepository : Repository<BookingDetail>, IBookingDetailRepository
    {
        public BookingDetailRepository(MovieTicketBookingDbcontext context) : base(context) { }
        public void Update(BookingDetail bookingDetail)
        {
            _context.Update(bookingDetail);
        }
    }
}
