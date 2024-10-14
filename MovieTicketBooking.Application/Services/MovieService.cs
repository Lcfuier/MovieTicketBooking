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
using MovieTicketBooking.Application.Common.Errors;
using AutoMapper;
namespace MovieTicketBooking.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _data;
        private readonly IMapper _mapper;
        public MovieService(IUnitOfWork data,IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }
        public async Task<Result<Movie?>> AddMovieAsync(MovieDTO movieDto)
        {
            QueryOptions<Movie> options = new QueryOptions<Movie>
            {
                Where = r => r.Title == movieDto.Title,
                Includes = "ShowTimes,Cinemas",
            };
            Movie? movie = await _data.Movie.GetAsync(options);
            if (movie != null)
            {
                string message = "movie is already exist.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail<Movie?>(new DuplicateError(message));
            }
            movie = _mapper.Map<Movie>(movieDto);
            if (movieDto.CinemaIds != null)
            {
                movie.Cinemas.Clear();
                foreach (var id in movieDto.CinemaIds)
                {
                    var cinema = await _data.Cinema.GetAsync(id);
                    if (cinema != null)
                    {
                        movie.Cinemas.Add(cinema);
                    }
                    else
                    {
                        string message = "Cinema not found.";
                        Log.Warning($"{this.GetType().Name} - {message} ");
                        return Result.Fail(new NotFoundError(message));
                    }
                }
            }
            if (movieDto.ShowTimeIds != null)
            {
                movie.ShowTimes?.Clear();
                foreach (var id in movieDto.ShowTimeIds)
                {
                    var showTime = await _data.ShowTime.GetAsync(id);
                    if (showTime != null)
                    {
                        movie.ShowTimes?.Add(showTime);
                    }
                    else
                    {
                        string message = "Show Time not found.";
                        Log.Warning($"{this.GetType().Name} - {message} ");
                        return Result.Fail(new NotFoundError(message));
                    }
                }
            }
            _data.Movie.Add(movie);
            await _data.SaveAsync();
            return movie;
        }

        public async Task<PaginationResponse<Movie>> GetAllMoviesAsync( int page )
        {
            QueryOptions<Movie> options = new QueryOptions<Movie>
            {
                Includes = "Cinemas,ShowTimes",
                PageNumber = page,
                PageSize = PagingConstants.DefaultPageSize
            };
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

        public async Task<Result<Movie?>> GetMovieByIdAsync(Guid id)
        {
            QueryOptions<Movie> options = new QueryOptions<Movie>
            {
                Includes = "Cinemas,ShowTimes",
                Where = mi => mi.MovieId==id
            };
            Movie? movie= await _data.Movie.GetAsync(options);
            if (movie == null)
            {
                string message = "Movie not found.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail<Movie?>(new NotFoundError(message));
            }
            return movie;
        }

        public Task<int> GetMovieCountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<PaginationResponse<Movie>> GetMoviesByTerm(string term, int page)
        {
            QueryOptions<Movie> options = new QueryOptions<Movie>
            {
                Includes = "Cinemas,ShowTimes",
            };
            if (term != null)
            {
                options.Where = mi => mi.Title.Contains(term);
            }
            var  movies = await _data.Movie.ListAllAsync(options);
            PaginationResponse<Movie> paginationResponse = new PaginationResponse<Movie>
            {
                PageNumber = page,
                PageSize = PagingConstants.DefaultPageSize,
                // must be above the TotalRecords bc it has multiple Where clauses
                Items = movies,
                TotalRecords = movies.Count()
            };
            return paginationResponse;
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

        public async Task<Result> UpdateMovieAsync(MovieDTO movieDto)
        {
            QueryOptions<Movie> options = new QueryOptions<Movie>
            {
                Where = r => r.MovieId == movieDto.MovieId,
                Includes = "Cinemas,ShowTimes",
            };
            Movie? movie = await _data.Movie.GetAsync(options, asNoTracking: true);
            if (movie == null)
            {
                string message = "Cinema not found.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail(new NotFoundError(message));
            }
            movie = _mapper.Map<Movie>(movieDto);
            if (movieDto.CinemaIds != null)
            {
                movie.Cinemas.Clear();
                foreach (var id in movieDto.CinemaIds)
                {
                    var cinema = await _data.Cinema.GetAsync(id);
                    if (cinema != null)
                    {
                        movie.Cinemas.Add(cinema);
                    }
                    else
                    {
                        string message = "Cinema not found.";
                        Log.Warning($"{this.GetType().Name} - {message} ");
                        return Result.Fail(new NotFoundError(message));
                    }
                }
            }
            if (movieDto.ShowTimeIds != null)
            {
                movie.ShowTimes?.Clear();
                foreach (var id in movieDto.ShowTimeIds)
                {
                    var showTime = await _data.ShowTime.GetAsync(id);
                    if (showTime != null)
                    {
                        movie.ShowTimes?.Add(showTime);
                    }
                    else
                    {
                        string message = "Show Time not found.";
                        Log.Warning($"{this.GetType().Name} - {message} ");
                        return Result.Fail(new NotFoundError(message));
                    }
                }
            }
            _data.Movie.Update(movie);
            await _data.SaveAsync();
            return Result.Ok();
        }
    }
}
