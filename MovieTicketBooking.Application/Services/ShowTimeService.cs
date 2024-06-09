using FluentResults;
using MovieTicketBooking.Application.DTOs;
using MovieTicketBooking.Application.Interfaces;
using MovieTicketBooking.Domain.Models;
using MovieTicketBooking.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Application.Services
{
    public class ShowTimeService : IShowTimeService
    {
        public Task AddShowTimeAsync(Cinema cinema)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Cinema>> AddShowTimeAsync(CinemaDTO cinema)
        {
            throw new NotImplementedException();
        }

        public Task<PaginationResponse<Cinema>> GetAllShowTimeAsync(int page)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Cinema?>> GetShowTimeByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetShowTimeCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result> RemoveShowTimeAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateShowTimeAsync(CinemaDTO cinema)
        {
            throw new NotImplementedException();
        }
    }
}
