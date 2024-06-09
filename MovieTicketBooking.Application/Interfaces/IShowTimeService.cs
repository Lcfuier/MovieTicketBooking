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
    public interface IShowTimeService
    {
        Task<PaginationResponse<Cinema>> GetAllShowTimeAsync(int page);
        Task<int> GetShowTimeCountAsync();

        Task<Result<Cinema?>> GetShowTimeByIdAsync(Guid id);

        Task AddShowTimeAsync(Cinema cinema);
        Task<Result<Cinema>> AddShowTimeAsync(CinemaDTO cinema);
        Task<Result> UpdateShowTimeAsync(CinemaDTO cinema);

        Task<Result> RemoveShowTimeAsync(Guid id);
    }
}
