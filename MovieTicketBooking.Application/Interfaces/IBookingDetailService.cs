using FluentResults;
using MovieTicketBooking.Application.DTOs;
using MovieTicketBooking.Domain.Models;
using MovieTicketBooking.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Application.Interfaces
{
    public interface IBookingDetailService
    {
        Task<Result<BookingDetail>> AddBookingDetailAsync(BookingDetailDTO bookingDetailDto);
        Task<PaginationResponse<BookingDetail>> GetAllBookingDetailAsync(int page);
        Task<PaginationResponse<BookingDetail>> GetAllBookingDetailAsync(int page, Guid BookingId);
        Task<Result> RemoveBookingDetailAsync(Guid id);
        Task<Result> UpdateBookingDetailAsync(BookingDetailDTO bookingDetailDto);
        Task<Result> UpdateBookingDetaiCheckOutlAsync(Guid id);
        Task<Result<BookingDetail>> GetBookingDetailByIdAsync(Guid Id);
    }
}
