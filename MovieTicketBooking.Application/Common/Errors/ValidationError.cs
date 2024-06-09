using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
namespace MovieTicketBooking.Application.Common.Errors
{
    public class ValidationError : Error
    {
        public ValidationError(string propertyName, string message)
        {
            PropertyName = propertyName;
            Message = message;
        }

        public string PropertyName { get; }
    }
}
