using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
namespace MovieTicketBooking.Application.Common.Errors
{
    public class DuplicateError : Error
    {
        public DuplicateError(string message)
        {
            Message = message;
        }
    }
}
