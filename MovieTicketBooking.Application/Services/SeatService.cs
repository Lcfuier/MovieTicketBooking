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
    public class SeatService : ISeatService
    {
        private readonly IUnitOfWork _data;
        private readonly IMapper _mapper;
        public SeatService(IUnitOfWork data, IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }
        public Task<Result<List<Seat>>> AddRangeSeatsAsync(List<SeatDTO> seats)
        {
            throw new NotImplementedException();
        }

        public Task AddSeatAsync(Seat seat)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Seat>> AddSeatAsync(SeatDTO seatDto)
        {
            QueryOptions<Seat> options = new QueryOptions<Seat>
            {
                Where = r => r.SeatNumber == seatDto.SeatNumber && r.ShowTimeId==seatDto.ShowTimeId,
            };
            Seat? seat = await _data.Seat.GetAsync(options,asNoTracking:true);
            if (seat != null)
            {
                string message = "Seat is already exist.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail<Seat>(new DuplicateError(message));
            }
            seat= _mapper.Map<Seat>(seatDto);   
            _data.Seat.Add(seat);
            await _data.SaveAsync();
            return seat;
        }

        public async Task<PaginationResponse<Seat>> GetAllSeatsAsync(int page)
        {
            QueryOptions<Seat> options = new QueryOptions<Seat>
            {
                
                PageNumber = page,
                PageSize = PagingConstants.DefaultPageSize
            };

            PaginationResponse<Seat> paginationResponse = new PaginationResponse<Seat>
            {
                PageNumber = page,
                PageSize = PagingConstants.DefaultPageSize,
                // must be above the TotalRecords bc it has multiple Where clauses
                Items = await _data.Seat.ListAllAsync(options),
                TotalRecords = await _data.Seat.CountAsync()
            };
            return paginationResponse;
        }

      

        public async Task<Result<Seat?>> GetSeatByIdAsync(Guid id)
        {
            QueryOptions<Seat> options = new QueryOptions<Seat>
            {
                Where = mi => mi.SeatId.Equals(id)
            };
            Seat? seat = await _data.Seat.GetAsync(options, asNoTracking: true);
            if (seat == null)
            {
                string message = "Seat not found.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail<Seat?>(new NotFoundError(message));
            }
            return seat;
        }

        public async Task<Result<Seat?>> GetSeatByShowTimeIdAsync(Guid id)
        {
            QueryOptions<Seat> options = new QueryOptions<Seat>
            {
                Where = mi => mi.ShowTimeId.Equals(id)
            };
            Seat? seat = await _data.Seat.GetAsync(options);
            if (seat == null)
            {
                string message = "Seat not found.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail<Seat?>(new NotFoundError(message));
            }


            return seat;
        }

        public async  Task<Result> RemoveSeatAsync(Guid id)
        {
            Seat? seat = await _data.Seat.GetAsync(id);
            if (seat == null)
            {
                string message = "Seat not found.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail(new NotFoundError(message));
            }
            _data.Seat.Remove(seat);
            await _data.SaveAsync();

            return Result.Ok();
        }

        public Task<Result<List<Seat>>> UpdateRangeSeatsAsync(List<SeatDTO> seats)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> UpdateSeatAsync(SeatDTO seatDto)
        {
            QueryOptions<Seat> options = new QueryOptions<Seat>
            {
                Where = r => r.SeatId == seatDto.SeatId,
                
            };
            Seat? seat = await _data.Seat.GetAsync(options, asNoTracking: true);
            if (seat == null)
            {
                string message = "Seat not found.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail(new NotFoundError(message));
            }
            seat = _mapper.Map<Seat>(seatDto);
            
            _data.Seat.Update(seat);
            await _data.SaveAsync();
            return Result.Ok();
        }
    }
}
