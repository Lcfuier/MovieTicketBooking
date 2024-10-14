using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Domain.Constants.Status
{
    public static class BookingStatus
    {
        public const string StatusProcessing = "Processing";
        public const string StatusCancelled = "Cancelled";
        public const string StatusBooked = "Booked";
    }
}
