using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Domain.Models
{
    public class Movie
    {
        [Key]
        public Guid MovieId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        [MaxLength(30)]
        public string Director { get; set; }
        [Required]
        public string Cast { get; set; }
        [Required]
        public string synopsis { get; set; }
        [Required]
        public string Duration { get; set; }
        [Required]
        public DateOnly ReleaseDate { get; set; }
        [Required]
        public string ImageUrl { get; set; }

        public DateTime? ModifiedTime { get; set; }
        public ICollection<ShowTime>? ShowTimes { get; set; }

        [ForeignKey("MovieId")]
        [InverseProperty("Movies")]
        public virtual ICollection<Cinema> Cinemas { get; set; } = new List<Cinema>();
    }
}
