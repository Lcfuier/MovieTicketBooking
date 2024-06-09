using FluentResults;
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
        Task<PaginationResponse<Movie>> GetAllMoviesAsync(Guid? cinemaId, int page);
        Task<IEnumerable<Movie>> GetMoviesByTerm(string term);
        Task<int> GetMovieCountAsync();

        Task<Movie?> GetMovieByIdAsync(Guid id);

        Task AddMovieAsync(Movie movie);

        Task UpdateMovieAsync(Movie movie);

        Task<Result> RemoveMovieAsync(Guid id);
    }
}
