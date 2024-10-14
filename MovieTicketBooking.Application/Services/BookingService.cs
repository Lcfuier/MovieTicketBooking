using AutoMapper;
using FluentResults;
using Microsoft.Extensions.Configuration;
using MovieTicketBooking.Application.Common.Errors;
using MovieTicketBooking.Application.DTOs;
using MovieTicketBooking.Domain.Constants.Status;
using MovieTicketBooking.Domain.Constants.VnPay;
using MovieTicketBooking.Domain.Interfaces;
using MovieTicketBooking.Domain.Models;
using MovieTicketBooking.Domain.Queries;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MovieTicketBooking.Application.Interfaces;
using System.Web.Http.Results;
using System.Reflection.Metadata;

namespace MovieTicketBooking.Application.Services
{
    public class BookingService :IBookingService
    {
        private readonly IUnitOfWork _data;
        private readonly IMapper _mapper;
        private readonly IBookingDetailService _bookingDetailService;
        private readonly ISeatService _SeatService;
        public BookingService(IUnitOfWork data, IMapper mapper,IBookingDetailService bookingDetailService,ISeatService seatService)
        {
            _data = data;
            _mapper = mapper;
            _bookingDetailService = bookingDetailService;  
            _SeatService = seatService;
        }
        public async Task<Result<Booking>> Checkout(BookingDTO bookingDto)
        {
            
            QueryOptions<Booking> options = new QueryOptions<Booking>
            {
                Where = r => r.BookingId == bookingDto.BookingId,
            };
            Booking? booking = await _data.Booking.GetAsync(options, asNoTracking: true);
            if (booking != null)
            {
                string message = "Booking is already booking.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail<Booking>(new DuplicateError(message));
            }
            booking = new Booking(); 
            booking.BookingId = bookingDto.BookingId;
            booking.CustomerId = bookingDto.CustomerId; 
            booking.Name= bookingDto.Name;
            booking.Email= bookingDto.Email;
            booking.PhoneNumber= bookingDto.PhoneNumber;
            booking.PaymentStatus = "Pending";
            booking.BookingStatus = BookingStatus.StatusProcessing;

            decimal amount = 0;
            foreach (var detail in bookingDto.BookingDetails)
            {
                var seat = _SeatService.GetSeatByIdAsync(detail.SeatId);
                var price = seat.Result.Value.Price;
                amount = amount + price;
            }
            booking.TotalAmount = amount;
            await AddBookingAsync(booking);
            //create detail
            foreach (var detail in bookingDto.BookingDetails)
            {
                if(detail.BookingId is null)
                {
                    detail.BookingId = booking.BookingId;
                }
                await _bookingDetailService.AddBookingDetailAsync(detail);
            }
            await _data.SaveAsync();
            return booking;
        }
        private async Task AddBookingAsync(Booking booking)
        {
            _data.Booking.Add(booking);
            await _data.SaveAsync();
        }
        public async Task<Result<Booking?>> GetBookingByIdAsync(Guid id)
        {
            QueryOptions<Booking> options = new QueryOptions<Booking>
            {
                Includes = "BookingDetail,Seat",
                Where = mi => mi.BookingId.Equals(id)
            };
            Booking? booking = await _data.Booking.GetAsync(options);
            if (booking == null)
            {
                string message = "Booking not found.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail<Booking?>(new NotFoundError(message));
            }
            return booking;
        }


        public async Task<Result> CheckoutUpdate(Guid id,VnPayResponeModel respone)
        {
            QueryOptions<Booking> options = new QueryOptions<Booking>
            {
                Includes = "BookingDetails",
                Where = mi => mi.BookingId.Equals(id)
            };
            Booking? booking = await _data.Booking.GetAsync(options);
            if (booking == null)
            {
                string message = "Booking not found.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail(new NotFoundError(message));
            }
            booking.BookingStatus=BookingStatus.StatusBooked;
            booking.PaymentStatus="Successful";
            booking.TransactionId=respone.TransactionId;
            foreach(var item in booking.BookingDetails)
            {
               await _bookingDetailService.UpdateBookingDetaiCheckOutlAsync(item.BookingDetailId); 
            }
            _data.Booking.Update(booking);
            await _data.SaveAsync();
            return Result.Ok();
        }
    }
        
    
}
