using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Application.Common.SuccessRusult
{
    public class SuccessResult : Success
    {
        public SuccessResult(string message)
        {
            Message = message;
        }
    }
}
