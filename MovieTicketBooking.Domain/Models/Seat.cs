using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Domain.Models
{
    public class Seat
    {
        [Key]
        public Guid SeatId { get; set; }
        [Required]
        [Range(1, 50)]
        public int SeatNumber { get; set; }
        public bool IsBooking { get; set; } = false;
        public Guid ShowTimeId { get; set; }
        public ShowTime ShowTime { get; set; }

    }
}
