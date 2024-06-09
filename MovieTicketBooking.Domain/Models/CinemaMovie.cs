using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Domain.Models
{
    public class CinemaMovie
    {
        [Key]
        [Required]
        public Guid MovieId { get; set; }
        [Key]
        [Required]
        public Guid CinemaId { get; set; }

        public Movie? Movie { get; set; }
        public Cinema? Cinema { get; set; }
    }
}
