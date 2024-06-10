using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MovieTicketBooking.Application.DTOs
{
    public class ShowTimeDTO
    {
        [Key]
        public Guid ShowTimeId { get; set; }
        [Required]
        public DateOnly DateShow { get; set; }
        [Required]
        public TimeOnly StartTime { get; set; }
        [Required]
        public TimeOnly EndTime { get; set; }
        public Guid? MovieId { get; set; }

    }
}
