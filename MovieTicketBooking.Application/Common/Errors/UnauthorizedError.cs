﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
namespace MovieTicketBooking.Application.Common.Errors
{
    public class UnauthorizedError : Error
    {
        public UnauthorizedError(string message)
        {
            Message = message;
        }
    }
}
