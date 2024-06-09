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
    public class MovieDTO
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
        public DateTime ReleaseDate { get; set; }
        [Required]
        public string ImageUrl { get; set; }

        public DateTime ModifiedTime { get; set; }
        public ICollection<ShowTime> ShowTimes { get; set; }
        public  ICollection<Cinema> Cinemas { get; set; }
    }
}
