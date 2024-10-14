using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _data;
        private readonly IMapper _mapper;
        private readonly UserManager<Customer> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IUserStore<Customer> _userStore;
        private readonly IUserEmailStore<Customer> _emailStore;
        public CustomerService(IUnitOfWork data, IMapper mapper, IUserStore<Customer> userStore, UserManager<Customer> userManager,RoleManager<IdentityRole> roleManager,IConfiguration configuration)
        {
            _data = data;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _userStore = userStore;
            _emailStore = GetEmailStore();
        }
        public Task<PaginationResponse<Customer>> GetAllCustomerAsync(int page)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Customer?>> GetCustomerByUserNameAsync(string userName)
        {
            QueryOptions<Customer> options = new QueryOptions<Customer>
            {
                Where = u => u.UserName == userName
            };
            Customer? isUserExist = await _data.Customer.GetAsync(options);
            if (isUserExist == null)
            {
                string message = "User doesn't exist.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail<Customer>(new NotFoundError(message));
            }
            return isUserExist;
        }
       
        public Task Login(CustomerDTO customer)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Customer>> Register(CustomerDTO customerDto,string? role)
        {
            QueryOptions<Customer> options = new QueryOptions<Customer>
            {
                Where = u => u.UserName == customerDto.UserName || u.Email == customerDto.Email
            };
            Customer? isUserExist = await _data.Customer.GetAsync(options);
            if (isUserExist != null)
            {
                string message = "Email or username is already exist.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail<Customer>(new NotFoundError(message));
            }
            var user = CreateUser();
            user.FirstName = customerDto.FirstName;
            user.LastName = customerDto.LastName;
            user.PhoneNumber = customerDto.PhoneNumber;
            await _userStore.SetUserNameAsync(user, customerDto.UserName, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, customerDto.Email, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, customerDto.Password);
            if (!result.Succeeded)
            {
                string message = "An error when created account.";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail<Customer>(new NotFoundError(message));
            }
            if (string.IsNullOrEmpty(role))
            {
                await _userManager.AddToRoleAsync(user, Roles.User);
            }
            else if (await _roleManager.RoleExistsAsync(role)){
                await _userManager.AddToRoleAsync(user, role);
            }
            else
            {
                string message = "This role does not exist !";
                Log.Warning($"{this.GetType().Name} - {message} ");
                return Result.Fail(new NotFoundError(message));
            }
            return user;
        }

        public Task<Result> RemoveCustomerAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateCustomerAsync(CustomerDTO customer)
        {
            throw new NotImplementedException();
        }
        private Customer CreateUser()
        {
            try
            {
                return Activator.CreateInstance<Customer>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(Customer)}'. " +
                    $"Ensure that '{nameof(Customer)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
        private IUserEmailStore<Customer> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<Customer>)_userStore;
        }

    }
}
