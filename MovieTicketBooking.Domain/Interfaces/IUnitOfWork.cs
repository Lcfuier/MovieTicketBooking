using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBookingRepository Booking {  get; }    
        ICinemaRepository Cinema { get; }
        ICustomerRepository Customer { get; }
        IMovieRepository Movie { get; }
        IShowTimeRepository ShowTime { get; }
        ISeatRepository Seat { get;  }
        IBookingDetailRepository BookingDetail { get; }
        Task SaveAsync();
    }
}
