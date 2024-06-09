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
    public class ShowTimeRepository : Repository<ShowTime>,IShowTimeRepository
    {
        public ShowTimeRepository(MovieTicketBookingDbcontext context) : base(context) { }
        public void Update(ShowTime showTime)
        {
            _context.Update(showTime);
        }
    }
}
