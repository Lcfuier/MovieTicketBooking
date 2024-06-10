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
    public class ShowTimeController : ApiController
    {
        private readonly IShowTimeService _showTimeService;
        private readonly IMapper _mapper;
        public ShowTimeController(IShowTimeService showTimeService, IMapper mapper)
        {
            _showTimeService = showTimeService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginationResponse<Cinema>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllShowTimes(int page = 1)
        {
            PaginationResponse<ShowTime> paginationResponse = await _showTimeService.GetAllShowTimeAsync(page);
            return Ok(paginationResponse);
        }
        [HttpGet("{id:guid}", Name = "GetShowTimeById")]
        [ProducesResponseType(typeof(ShowTime), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetShowTimeById(Guid id)
        {
            Result<ShowTime?> getShowTimeResult = await _showTimeService.GetShowTimeByIdAsync(id);
            if (getShowTimeResult.IsFailed)
            {
                return Problem(getShowTimeResult.Errors);
            }

            return Ok(getShowTimeResult.Value);
        }
        [HttpPost]
        [ProducesResponseType(typeof(ShowTimeDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateShowTime([FromForm] ShowTimeDTO showTimeDTO)
        {
            Result<ShowTime> createShowTimeResult = await _showTimeService.AddShowTimeAsync(showTimeDTO);
            if (createShowTimeResult.IsFailed)
            {
                return Problem(createShowTimeResult.Errors);
            }

            return CreatedAtRoute(
                nameof(GetShowTimeById),
                new { Id = createShowTimeResult.Value.ShowTimeId},
                createShowTimeResult.Value);
        }
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateShowTime(Guid id, [FromForm] ShowTimeDTO showTimeDTO)
        {
            if (id != showTimeDTO.ShowTimeId)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Id not match.");
            }

            Result updateShowTimeResult = await _showTimeService.UpdateShowTimeAsync(showTimeDTO);
            if (updateShowTimeResult.IsFailed)
            {
                return Problem(updateShowTimeResult.Errors);
            }

            return NoContent();
        }
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> RemoveShowTime(Guid id)
        {
            Result RemoveShowTimeResult = await _showTimeService.RemoveShowTimeAsync(id);
            if (RemoveShowTimeResult.IsFailed)
            {
                return Problem(RemoveShowTimeResult.Errors);
            }

            return NoContent();
        }
    }
}
