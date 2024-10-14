using MovieTicketBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Application.DTOs
{
    public class BookingDetailDTO
    {
        public Guid BookingDetailId { get; set; }

        public Guid? CinemaId { get; set; }


        public Guid? MovieId { get; set; }


        public Guid SeatId { get; set; }


        public Guid? ShowTimeId { get; set; }


        public Guid? BookingId { get; set; }

    }
}
