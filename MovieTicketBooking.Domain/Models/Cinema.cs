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
    public class Cinema
    {
        [Key]
        public Guid CinemaId { get; set; }
        [Required]
        [MaxLength(100)]
        public string CinemaName { get; set; }
        [ForeignKey("CinemaId")]
        [InverseProperty("Cinemas")]
        [JsonIgnore]
        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
