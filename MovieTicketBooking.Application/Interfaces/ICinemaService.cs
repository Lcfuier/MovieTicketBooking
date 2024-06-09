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
    public interface ICinemaService
    {
        Task<PaginationResponse<Cinema>> GetAllCinemasAsync( int page);
        Task<PaginationResponse<Cinema>> GetCinemasByTerm(string term,int page);
        Task<int> GetCinemaCountAsync();

        Task<Result<Cinema?>> GetCinemaByIdAsync(Guid id);

        Task AddCinemaAsync(Cinema cinema);
        Task<Result<Cinema>> AddCinemaAsync(CinemaDTO cinema);
        Task<Result> UpdateCinemaAsync(CinemaDTO cinema);

        Task<Result> RemoveCinemaAsync(Guid id);
    }
}
