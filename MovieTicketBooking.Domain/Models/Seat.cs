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
    public class Seat
    {
        [Key]
        public Guid SeatId { get; set; }
        [Required]
        [Range(1, 50)]
        public int SeatNumber { get; set; }
        public bool IsBooking { get; set; } = false;
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        [StringLength(128)]
        public Guid? ShowTimeId { get; set; }
        [JsonIgnore]
        public ShowTime? ShowTime { get; set; }

    }
}
