using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using FluentResults;
namespace MovieTicketBooking.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _data;
        public MovieService(IUnitOfWork data)
        {
            _data = data;
        }
        public Task AddMovieAsync(Movie movie)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginationResponse<Movie>> GetAllMoviesAsync(Guid? cinemaId, int page )
        {
            QueryOptions<Movie> options = new QueryOptions<Movie>
            {
                Includes = "Cinema,ShowTime",
                PageNumber = page,
                PageSize = PagingConstants.DefaultPageSize
            };
            if (cinemaId != null)
            {
                options.Where = mi => mi.Cinemas.Any(c=>c.CinemaId.Equals(cinemaId));
            }
            PaginationResponse<Movie> paginationResponse = new PaginationResponse<Movie>
            {
                PageNumber = page,
                PageSize = PagingConstants.DefaultPageSize,
                // must be above the TotalRecords bc it has multiple Where clauses
                Items = await _data.Movie.ListAllAsync(options),
                TotalRecords = await _data.Movie.CountAsync()
            };
            return paginationResponse;
        }

        public async Task<Movie?> GetMovieByIdAsync(Guid id)
        {
            QueryOptions<Movie> options = new QueryOptions<Movie>
            {
                Includes = "Cinema,ShowTime",
                Where = mi => mi.MovieId==id
            };
            Movie? movie= await _data.Movie.GetAsync(options);
            if (movie == null)
            {
                string message = "Movie not found.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(message, System.Text.Encoding.UTF8, "text/plain"),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);
            }
            

            return movie;
        }

        public Task<int> GetMovieCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetMoviesByTerm(string term)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> RemoveMovieAsync(Guid id)
        {
            Movie? movie = await _data.Movie.GetAsync(id);
            if (movie == null)
            {
                string message = "MenuItem not found.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(message, System.Text.Encoding.UTF8, "text/plain"),
                    StatusCode = HttpStatusCode.NotFound
                };
                throw new HttpResponseException(response);
            }

             _data.Movie.Remove(movie);
            await _data.SaveAsync();

            return Result.Ok();
        }

        public Task UpdateMovieAsync(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
