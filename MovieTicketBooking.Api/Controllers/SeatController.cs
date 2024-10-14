using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Application.DTOs;
using MovieTicketBooking.Application.Interfaces;
using MovieTicketBooking.Application.Services;
using MovieTicketBooking.Domain.Models;
using MovieTicketBooking.Domain.Queries;

namespace MovieTicketBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeatController : ApiController
    {
        private readonly ISeatService _seatService;
        private readonly IMapper _mapper;
        public SeatController(ISeatService seatService, IMapper mapper)
        {
            _seatService = seatService;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(typeof(PaginationResponse<Seat>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSeats(int page = 1)
        {
            PaginationResponse<Seat> paginationResponse = await _seatService.GetAllSeatsAsync(page);
            return Ok(paginationResponse);
        }
        [HttpGet("{id:guid}", Name = "GetSeatById")]
        [ProducesResponseType(typeof(Seat), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSeatById(Guid id)
        {
            Result<Seat?> getSeatResult = await _seatService.GetSeatByIdAsync(id);
            if (getSeatResult.IsFailed)
            {
                return Problem(getSeatResult.Errors);
            }

            return Ok(getSeatResult.Value);
        }
        [HttpPost]
        [ProducesResponseType(typeof(SeatDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateSeat([FromForm] SeatDTO seatDTO)
        {
            Result<Seat> createSeatResult = await _seatService.AddSeatAsync(seatDTO);
            if (createSeatResult.IsFailed)
            {
                return Problem(createSeatResult.Errors);
            }

            return CreatedAtRoute(
                nameof(GetSeatById),
                new { Id = createSeatResult.Value.SeatId },
                createSeatResult.Value);
        }
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateSeat(Guid id, [FromForm] SeatDTO seatDTO)
        {
            if (id != seatDTO.SeatId)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Id not match.");
            }

            Result updateSeatResult = await _seatService.UpdateSeatAsync(seatDTO);
            if (updateSeatResult.IsFailed)
            {
                return Problem(updateSeatResult.Errors);
            }

            return NoContent();
        }
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> RemoveSeat(Guid id)
        {
            Result RemoveSeatResult = await _seatService.RemoveSeatAsync(id);
            if (RemoveSeatResult.IsFailed)
            {
                return Problem(RemoveSeatResult.Errors);
            }

            return NoContent();
        }
    }
}
