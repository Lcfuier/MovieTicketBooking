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
    public class CinemaDTO
    {
        [Key]
        public Guid CinemaId { get; set; }
        [Required]
        [MaxLength(100)]
        public string CinemaName { get; set; }
        public Guid[]? MovieIds { get; set; } = null!;



    }
}
