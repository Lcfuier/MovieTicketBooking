using MovieTicketBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Domain.Interfaces
{
    public interface IShowTimeRepository : IRepository<ShowTime>
    {
        void Update(ShowTime showTime);
    }
}
