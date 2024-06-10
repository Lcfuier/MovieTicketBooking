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
    public interface ISeatService
    {
        Task<PaginationResponse<Seat>> GetAllSeatsAsync(int page);


        Task<Result<Seat?>> GetSeatByIdAsync(Guid id);
        Task<Result<Seat?>> GetSeatByShowTimeIdAsync(Guid id);

        Task AddSeatAsync(Seat seat);
        Task<Result<Seat>> AddSeatAsync(SeatDTO seat);
        Task<Result> UpdateSeatAsync(SeatDTO seat);

        Task<Result> RemoveSeatAsync(Guid id);
        Task<Result<List<Seat>>> AddRangeSeatsAsync(List<SeatDTO> seats);
        Task<Result<List<Seat>>> UpdateRangeSeatsAsync(List<SeatDTO> seats);
    }
}
