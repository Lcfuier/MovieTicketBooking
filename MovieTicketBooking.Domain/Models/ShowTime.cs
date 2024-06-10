using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
      
        public Guid? MovieId { get; set; }
        [JsonIgnore]
        public Movie? Movie { get; set; }
        public ICollection<Seat>? Seats{ get; set; }

    }
}
