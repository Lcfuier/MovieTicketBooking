using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MovieTicketBooking.Application.DTOs
{
    public class BookingDTO
    {
        public Guid BookingId { get; set; }
        [StringLength(128)]
        public string? Name { get; set; }
        [StringLength(15)]
        [Unicode(false)]
        public string PhoneNumber { get; set; } = null!;
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }
        public string? CustomerId { get; set; }
        public IEnumerable<BookingDetailDTO>? BookingDetails { get; set; }
    }
}
