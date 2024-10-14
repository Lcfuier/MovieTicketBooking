using FluentResults;
using Microsoft.AspNetCore.Http;
using MovieTicketBooking.Application.DTOs;
using MovieTicketBooking.Domain.Constants.VnPay;
using MovieTicketBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Application.Interfaces
{
    public interface IBookingService
    {
        Task<Result<Booking>> Checkout(BookingDTO bookingDto);
        Task<Result<Booking?>> GetBookingByIdAsync(Guid id);
        Task<Result> CheckoutUpdate(Guid id, VnPayResponeModel respone);
    }
}
