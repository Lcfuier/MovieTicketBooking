using MovieTicketBooking.Domain.Interfaces;
using MovieTicketBooking.Infrastructure.Data;
using MovieTicketBooking.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieTicketBookingDbcontext _context;
        public IBookingRepository Booking { get; private set; }
        public IBookingDetailRepository BookingDetail { get; private set; }

        public ICinemaRepository Cinema { get; private set; }

        public ICustomerRepository Customer { get; private set; }

        public IMovieRepository Movie { get; private set; }

        public IShowTimeRepository ShowTime { get; private set; }
        public ISeatRepository Seat { get; private set; }
        public UnitOfWork(MovieTicketBookingDbcontext context)
        {
            _context = context;
            Movie=new MovieRepository(_context);
            Cinema=new CinemaRepository(_context);
            ShowTime=new ShowTimeRepository(_context);
            Booking=new BookingRepository(_context);
            Customer=new CustomerRepository(_context);
            Seat=new SeatRepository(_context);
            BookingDetail=new BookingDetailRepository(_context);
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
