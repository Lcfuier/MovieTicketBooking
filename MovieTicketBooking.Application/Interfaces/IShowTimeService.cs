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
        Task<PaginationResponse<ShowTime>> GetAllShowTimeAsync(int page);
        Task<int> GetShowTimeCountAsync();

        Task<Result<ShowTime?>> GetShowTimeByIdAsync(Guid id);

        Task AddShowTimeAsync(ShowTime showTime);
        Task<Result<ShowTime>> AddShowTimeAsync(ShowTimeDTO showTimee);
        Task<Result> UpdateShowTimeAsync(ShowTimeDTO showTime);

        Task<Result> RemoveShowTimeAsync(Guid id);
    }
}
