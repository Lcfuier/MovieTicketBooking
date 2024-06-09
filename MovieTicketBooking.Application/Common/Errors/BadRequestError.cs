using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
namespace MovieTicketBooking.Application.Common.Errors
{
    public class BadRequestError : Error
    {
        public BadRequestError(string message)
        {
            Message = message;
        }
    }
}
