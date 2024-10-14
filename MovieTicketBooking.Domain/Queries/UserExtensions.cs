using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Domain.Queries
{
    public static class UserExtensions
    {
        public static string GetCurrentUserId(this ClaimsPrincipal user)
        {
            return (user.FindFirst(ClaimTypes.NameIdentifier)?.Value.ToString() ?? string.Empty);
        }
    }
}
