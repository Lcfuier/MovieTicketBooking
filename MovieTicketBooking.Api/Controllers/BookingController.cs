using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MovieTicketBooking.Application.DTOs;
using MovieTicketBooking.Application.Interfaces;
using MovieTicketBooking.Application.Services;
using MovieTicketBooking.Domain.Constants.VnPay;
using MovieTicketBooking.Domain.Interfaces;
using MovieTicketBooking.Domain.Models;
using MovieTicketBooking.Domain.Queries;
using System.Security.Claims;

namespace MovieTicketBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ApiController
    {
        private readonly IBookingService _bookingService;
        private readonly IBookingDetailService _bookingDetailService;
        private readonly IVnPayService _vnPayService;
        private readonly ICustomerService _customerService;
        public BookingController(IBookingService bookingService,IVnPayService vnPayService,ICustomerService customerService,IBookingDetailService bookingDetailService)
        {
            _bookingService = bookingService;
            _vnPayService = vnPayService;
            _customerService = customerService;
            _bookingDetailService = bookingDetailService;
        }
        [HttpPost("checkout-vnpay")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> CheckoutVnpay([FromBody] BookingDTO bookingDto)
        {
            ClaimsIdentity? claimsIdentity = User.Identity as ClaimsIdentity;
            string Username = claimsIdentity.Name.ToString(); 
            /*var user = await _customerService.GetCustomerByIdAsync(claim?.Value)
                ?? throw new Exception("User not found.");*/
            var user = await _customerService.GetCustomerByUserNameAsync(Username);
            bookingDto.CustomerId =user.Value.Id;

            Result<Booking> createBookingResult = await _bookingService.Checkout(bookingDto);
            if (createBookingResult.IsFailed)
            {
                return Problem(createBookingResult.Errors);
            }
            var vnpayRequest = new VnPayRequestModel()
            {
                Amount=createBookingResult.Value.TotalAmount,
                OrderId=createBookingResult.Value.BookingId.ToString(),
                Description= createBookingResult.Value.BookingId.ToString()
            };
            return Ok(_vnPayService.CreatePaymentUrl(HttpContext, vnpayRequest));
        }
        [AllowAnonymous]
        [HttpGet("vnpay-return")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PaymentCallBack()
        {
            VnPayResponeModel? vnpayResponse = _vnPayService.PaymentExcute(Request.Query);
            if (vnpayResponse == null)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "can't create the response.");
            }
            var result=await _bookingService.CheckoutUpdate(Guid.Parse(vnpayResponse.BookingId),vnpayResponse);

            if (result.IsFailed)
            {
                return Problem(result.Errors);
            }

           return NoContent();
        }
    }
}
