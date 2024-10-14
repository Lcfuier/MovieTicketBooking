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
    public interface IMovieService
    {
        Task<PaginationResponse<Movie>> GetAllMoviesAsync( int page);
        Task<PaginationResponse<Movie>> GetMoviesByTerm(string term, int page);
        Task<int> GetMovieCountAsync();

        Task<Result<Movie?>> GetMovieByIdAsync(Guid id);

        Task<Result<Movie?>> AddMovieAsync(MovieDTO movie);

        Task<Result> UpdateMovieAsync(MovieDTO movie);

        Task<Result> RemoveMovieAsync(Guid id);
    }
}
