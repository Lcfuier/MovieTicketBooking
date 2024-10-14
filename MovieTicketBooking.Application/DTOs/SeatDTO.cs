using MovieTicketBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Application.DTOs
{
    public class SeatDTO
    {
        [Key]
        public Guid SeatId { get; set; }
        [Required]
        [Range(1, 50)]
        public int SeatNumber { get; set; }
        public bool IsBooking { get; set; } = false;
        public decimal Price  { get; set; }

        public Guid? ShowTimeId { get; set; }
    }
}
