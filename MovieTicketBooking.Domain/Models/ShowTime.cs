using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MovieTicketBooking.Domain.Models
{
    public class ShowTime
    {
        [Key]
        public Guid ShowTimeId { get; set; }
        [Required]
        public DateOnly DateShow { get; set; }
        [Required]
        public TimeOnly StartTime { get; set; }
        [Required]
        public TimeOnly EndTime { get; set; }
        [Required]
        [Range(1,50)]
        public int Seats { get; set; }
        public bool IsBooking { get; set; } = false;
        public Guid MovieId { get; set; }
        public Movie? Movie { get; set; }

    }
}
