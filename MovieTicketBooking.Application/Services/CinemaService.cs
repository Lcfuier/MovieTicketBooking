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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MovieTicketBooking.Application.Services
{
    public class CinemaService : ICinemaService
    {
        private readonly IUnitOfWork _data;
        private readonly IMapper _mapper;
        public CinemaService(IUnitOfWork data,IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }
        public Task AddCinemaAsync(Cinema cinema)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Cinema>> AddCinemaAsync(CinemaDTO cinemaDto)
        {
            QueryOptions<Cinema> options = new QueryOptions<Cinema>
            {
                Where = r => r.CinemaName == cinemaDto.CinemaName,
                Includes="Movies",
            };
            Cinema? cinema = await _data.Cinema.GetAsync(options);
            if (cinema != null)
            {
                string message = "Cinema is already exist.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail<Cinema>(new DuplicateError(message));
            }
            cinema = new Cinema()
            {
                
                CinemaName = cinemaDto.CinemaName
            };
            if (cinemaDto.CinemaId == null)
            {
                cinema.CinemaId = new Guid();
            }
            else
                cinema.CinemaId = cinemaDto.CinemaId;
            if (cinemaDto.MovieIds != null)
            {
                foreach(var id in cinemaDto.MovieIds)
                {
                    var movie = await _data.Movie.GetAsync(id);
                    if (movie != null)
                    {
                        cinema.Movies.Add(movie);
                    }
                    else
                    {
                        string message = "Movie not found.";
                        Log.Warning($"{this.GetType().Name} - {message} ");
                        return Result.Fail<Cinema>(new NotFoundError(message));
                    }
                }
            }
            _data.Cinema.Add(cinema);
            await _data.SaveAsync();
            return cinema;
        }

        public async Task<PaginationResponse<Cinema>> GetAllCinemasAsync(int page)
        {
            QueryOptions<Cinema> options = new QueryOptions<Cinema>
            {
                Includes = "Movies",
                PageNumber = page,
                PageSize = PagingConstants.DefaultPageSize
            };

            PaginationResponse<Cinema> paginationResponse = new PaginationResponse<Cinema>
            {
                PageNumber = page,
                PageSize = PagingConstants.DefaultPageSize,
                // must be above the TotalRecords bc it has multiple Where clauses
                Items = await _data.Cinema.ListAllAsync(options),
                TotalRecords = await _data.Cinema.CountAsync()
            };
            return paginationResponse;
        }

        public async Task<Result<Cinema?>> GetCinemaByIdAsync(Guid id)
        {
            QueryOptions<Cinema> options = new QueryOptions<Cinema>
            {
                Includes = "Movies",
                Where = mi => mi.CinemaId.Equals(id)
            };
            Cinema? cinema = await _data.Cinema.GetAsync(options);
            if (cinema == null)
            {
                string message = "Cinema not found.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail<Cinema?>(new NotFoundError(message));
            }


            return cinema;
        }

        public Task<int> GetCinemaCountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<PaginationResponse<Cinema>> GetCinemasByTerm(string term,int page)
        {
            QueryOptions<Cinema> options = new QueryOptions<Cinema>
            {
                Includes = "Movie",
            };
            if (term!=null)
            {
                options.Where = mi => mi.CinemaName.Contains(term);
            }
            var cinemas = await _data.Cinema.ListAllAsync(options);
            PaginationResponse<Cinema> paginationResponse = new PaginationResponse<Cinema>
            {
                PageNumber = page,
                PageSize = PagingConstants.DefaultPageSize,
                // must be above the TotalRecords bc it has multiple Where clauses
                Items = cinemas,
                TotalRecords = cinemas.Count()
            };
            return paginationResponse;
        }

        public async Task<Result> RemoveCinemaAsync(Guid id)
        {
            Cinema? cinema = await _data.Cinema.GetAsync(id);
            if (cinema == null)
            {
                string message = "Cinema not found.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail(new NotFoundError(message));
            }

            _data.Cinema.Remove(cinema);
            await _data.SaveAsync();

            return Result.Ok();
        }

        public async Task<Result> UpdateCinemaAsync(CinemaDTO cinemaRequest)
        {
            QueryOptions<Cinema> options = new QueryOptions<Cinema>
            {
                Where = r => r.CinemaId == cinemaRequest.CinemaId,
                Includes = "Movies",
            };
            Cinema? cinema = await _data.Cinema.GetAsync(options,asNoTracking:true);
            if(cinema== null)
            {
                string message = "Cinema not found.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail(new NotFoundError(message));
            }
            cinema = _mapper.Map<Cinema>(cinemaRequest);
            if (cinemaRequest.MovieIds != null)
            {
                foreach (var id in cinemaRequest.MovieIds)
                {
                    var movie = await _data.Movie.GetAsync(id);
                    if (movie != null)
                    {
                        cinema.Movies.Add(movie);
                    }
                    else
                    {
                        string message = "Movie not found.";
                        Log.Warning($"{this.GetType().Name} - {message} ");
                        return Result.Fail(new NotFoundError(message));
                    }
                }
            }
            _data.Cinema.Update(cinema);
            await _data.SaveAsync();
            return Result.Ok();
        }
    }
}
