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
    public interface ICustomerService
    {
        Task<PaginationResponse<Customer>> GetAllCustomerAsync(int page);

        Task<Result<Customer?>> GetCustomerByUserNameAsync(string id);

        Task<Result<Customer>> Register(CustomerDTO customer, string role);
        Task Login(CustomerDTO customer);

        Task<Result> UpdateCustomerAsync(CustomerDTO customer);

        Task<Result> RemoveCustomerAsync(Guid id);
    }
}
