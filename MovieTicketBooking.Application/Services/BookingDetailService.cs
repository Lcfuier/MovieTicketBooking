using AutoMapper;
using FluentResults;
using MovieTicketBooking.Application.Common.Errors;
using MovieTicketBooking.Application.DTOs;
using MovieTicketBooking.Application.Interfaces;
using MovieTicketBooking.Domain.Constants;
using MovieTicketBooking.Domain.Interfaces;
using MovieTicketBooking.Domain.Models;
using MovieTicketBooking.Domain.Queries;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Application.Services
{
    public class BookingDetailService : IBookingDetailService
    {
        private readonly IUnitOfWork _data;
        private readonly IMapper _mapper;
        public BookingDetailService(IUnitOfWork data, IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }
        public async Task<Result<BookingDetail>> AddBookingDetailAsync(BookingDetailDTO bookingDetailDto)
        {
            QueryOptions<BookingDetail> options = new QueryOptions<BookingDetail>
            {
                Where = r => r.SeatId == bookingDetailDto.SeatId,
                Includes = "Movie,Seat,ShowTime,Cinema",
            };
            BookingDetail? bookingDetail = await _data.BookingDetail.GetAsync(options,asNoTracking:true);
            if (bookingDetail != null)
            {
                string message = "Seat is already booking.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail<BookingDetail>(new DuplicateError(message));
            }
            bookingDetail = _mapper.Map<BookingDetail>(bookingDetailDto);
            if (bookingDetailDto.BookingDetailId == null)
            {
                bookingDetail.BookingDetailId = new Guid();
            }
            _data.BookingDetail.Add(bookingDetail);
            await _data.SaveAsync();
            return bookingDetail;
        }
        public async Task<PaginationResponse<BookingDetail>> GetAllBookingDetailAsync(int page)
        {
            QueryOptions<BookingDetail> options = new QueryOptions<BookingDetail>
            {
                Includes = "Movie,Seat,ShowTime,Cinema",
                PageNumber = page,
                PageSize = PagingConstants.DefaultPageSize
            };

            PaginationResponse<BookingDetail> paginationResponse = new PaginationResponse<BookingDetail>
            {
                PageNumber = page,
                PageSize = PagingConstants.DefaultPageSize,
                // must be above the TotalRecords bc it has multiple Where clauses
                Items = await _data.BookingDetail.ListAllAsync(options),
                TotalRecords = await _data.BookingDetail.CountAsync()
            };
            return paginationResponse;
        }
        public async Task<PaginationResponse<BookingDetail>> GetAllBookingDetailAsync(int page,Guid BookingId)
        {
            QueryOptions<BookingDetail> options = new QueryOptions<BookingDetail>
            {
                Where=r=>r.BookingId== BookingId,
                Includes = "Movie,Seat,ShowTime,Cinema",
                PageNumber = page,
                PageSize = PagingConstants.DefaultPageSize
            };

            PaginationResponse<BookingDetail> paginationResponse = new PaginationResponse<BookingDetail>
            {
                PageNumber = page,
                PageSize = PagingConstants.DefaultPageSize,
                // must be above the TotalRecords bc it has multiple Where clauses
                Items = await _data.BookingDetail.ListAllAsync(options),
                TotalRecords = await _data.BookingDetail.CountAsync()
            };
            return paginationResponse;
        }
        public async Task<Result> RemoveBookingDetailAsync(Guid id)
        {
            BookingDetail? bookingDetail = await _data.BookingDetail.GetAsync(id);
            if (bookingDetail == null)
            {
                string message = "Booking Detaiil not found.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail(new NotFoundError(message));
            }

            _data.BookingDetail.Remove(bookingDetail);
            await _data.SaveAsync();

            return Result.Ok();
        }
        public async Task<Result> UpdateBookingDetailAsync(BookingDetailDTO bookingDetailDto)
        {
            QueryOptions<BookingDetail> options = new QueryOptions<BookingDetail>
            {
                Where = r => r.BookingDetailId == bookingDetailDto.BookingDetailId,
            };
            BookingDetail? bookingDetail = await _data.BookingDetail.GetAsync(options, asNoTracking: true);
            if (bookingDetail == null)
            {
                string message = "BookingDetail not found.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail(new NotFoundError(message));
            }
            bookingDetail = _mapper.Map<BookingDetail>(bookingDetail);

            _data.BookingDetail.Update(bookingDetail);
            await _data.SaveAsync();
            return Result.Ok();
        }
        public async Task<Result> UpdateBookingDetaiCheckOutlAsync(Guid id)
        {
            QueryOptions<BookingDetail> options = new QueryOptions<BookingDetail>
            {
                Where = r => r.BookingDetailId == id,
                Includes = "Seat"
            };
            BookingDetail? bookingDetail = await _data.BookingDetail.GetAsync(options, asNoTracking: true);
            if (bookingDetail == null)
            {
                string message = "BookingDetail not found.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail(new NotFoundError(message));
            }
            var seat=bookingDetail.Seat;
            seat.IsBooking = true;

            _data.Seat.Update(seat);
            await _data.SaveAsync();
            return Result.Ok();
        }

        public async Task<Result<BookingDetail>> GetBookingDetailByIdAsync(Guid Id)
        {
            QueryOptions<BookingDetail> options = new QueryOptions<BookingDetail>
            {
                Where = r => r.BookingDetailId== Id,
                Includes = "Movie,Seat,ShowTime,Cinema",
                
            };
            BookingDetail? bookingDetail = await _data.BookingDetail.GetAsync(options, asNoTracking: false);
            if (bookingDetail == null)
            {
                string message = "BookingDetail not found.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail<BookingDetail>(new NotFoundError(message));
            }
            return bookingDetail;
        }
    }
}
