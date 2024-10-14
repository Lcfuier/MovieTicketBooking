using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MovieTicketBooking.Domain.Models
{
    public class BookingDetail
    {
        [Key]
        public Guid BookingDetailId { get; set; }

        public Guid? CinemaId { get; set; }
        [ForeignKey(nameof(CinemaId))]
        public Cinema? Cinema { get; set; }

        public Guid? MovieId { get; set; }
        [ForeignKey(nameof(MovieId))]
        public Movie? Movie { get; set; }

        public Guid? SeatId{ get; set; }
        [ForeignKey(nameof(SeatId))]
        public Seat? Seat { get; set; }

        public Guid? ShowTimeId { get; set; }
        [ForeignKey(nameof(ShowTimeId))]
        public ShowTime? ShowTime { get; set; }

        
        public Guid? BookingId { get; set; }
        [ForeignKey(nameof(BookingId))]
        [JsonIgnore]
        public Booking? Booking { get; set; }

    }
}
