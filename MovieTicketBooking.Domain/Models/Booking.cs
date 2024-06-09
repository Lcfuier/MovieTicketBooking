using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Domain.Models
{
    public class Booking
    {
        [Key]
        public Guid BookingId { get; set; }
        [Column(TypeName = "money")]
        public decimal TotalAmount { get; set; }
        [StringLength(128)]
        public string? Name { get; set; }
        [StringLength(15)]
        [Unicode(false)]
        public string PhoneNumber { get; set; } = null!;
        [StringLength(20)]
        [Unicode(false)]
        public string? PaymentStatus { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        public string CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }

        public virtual ICollection<Cinema> Cinemas { get; set; } = new List<Cinema>();
    }
}
