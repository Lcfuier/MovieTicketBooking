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
 
    public class ShowTimeService : IShowTimeService
    {
        private readonly IUnitOfWork _data;
        private readonly IMapper _mapper;
        public ShowTimeService(IUnitOfWork data,IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }
        public Task AddShowTimeAsync(ShowTime showTime)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<ShowTime>> AddShowTimeAsync(ShowTimeDTO showTimeDto)
        {
            QueryOptions<ShowTime> options = new QueryOptions<ShowTime>
            {
                Where = r => r.DateShow == showTimeDto.DateShow && r.StartTime==showTimeDto.StartTime,
                Includes = "Movie",
            };
            ShowTime? showTime = await _data.ShowTime.GetAsync(options);
            if (showTime != null)
            {
                string message = "Show Time is already exist.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail<ShowTime>(new DuplicateError(message));
            }
            showTime =_mapper.Map<ShowTime>(showTimeDto);
            if (showTimeDto.ShowTimeId== null)
            {
                showTime.ShowTimeId = new Guid();
            }
            
            
            _data.ShowTime.Add(showTime);
            await _data.SaveAsync();
            return showTime;
        }

        public async Task<PaginationResponse<ShowTime>> GetAllShowTimeAsync(int page)
        {
            QueryOptions<ShowTime> options = new QueryOptions<ShowTime>
            {
                Includes = "Movie,Seats",
                PageNumber = page,
                PageSize = PagingConstants.DefaultPageSize
            };

            PaginationResponse<ShowTime> paginationResponse = new PaginationResponse<ShowTime>
            {
                PageNumber = page,
                PageSize = PagingConstants.DefaultPageSize,
                // must be above the TotalRecords bc it has multiple Where clauses
                Items = await _data.ShowTime.ListAllAsync(options),
                TotalRecords = await _data.ShowTime.CountAsync()
            };
            return paginationResponse;
        }

        public async Task<Result<ShowTime?>> GetShowTimeByIdAsync(Guid id)
        {
            QueryOptions<ShowTime> options = new QueryOptions<ShowTime>
            {
                Includes = "Movie",
                Where = mi => mi.ShowTimeId.Equals(id)
            };
            ShowTime? showTime = await _data.ShowTime.GetAsync(options);
            if (showTime == null)
            {
                string message = "Show Time not found.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail<ShowTime?>(new NotFoundError(message));
            }


            return showTime;
        }

        public Task<int> GetShowTimeCountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Result> RemoveShowTimeAsync(Guid id)
        {
            ShowTime? showTime= await _data.ShowTime.GetAsync(id);
            if (showTime == null)
            {
                string message = "Show Time not found.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail(new NotFoundError(message));
            }

            _data.ShowTime.Remove(showTime);
            await _data.SaveAsync();

            return Result.Ok();
        }

        public async Task<Result> UpdateShowTimeAsync(ShowTimeDTO showTimeDto)
        {
            QueryOptions<ShowTime> options = new QueryOptions<ShowTime>
            {
                Where = r => r.ShowTimeId == showTimeDto.ShowTimeId,
                Includes = "Movie",
            };
            ShowTime? showTime = await _data.ShowTime.GetAsync(options, asNoTracking: true);
            if (showTime == null)
            {
                string message = "Show Time not found.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail(new NotFoundError(message));
            }
            showTime = _mapper.Map<ShowTime>(showTimeDto);
            
            _data.ShowTime.Update(showTime);
            await _data.SaveAsync();
            return Result.Ok();
        }
    }
}
