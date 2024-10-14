using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Application.DTOs;
using MovieTicketBooking.Application.Interfaces;
using MovieTicketBooking.Domain.Models;
using MovieTicketBooking.Domain.Queries;

namespace MovieTicketBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CinemaController : ApiController
    {
        private readonly ICinemaService _cinemaService;
        private readonly IMapper _mapper;
        public  CinemaController(ICinemaService cinemaService,IMapper mapper)
        {
            _cinemaService=cinemaService ;
            _mapper=mapper ;
        }
        [HttpGet]
        [ProducesResponseType(typeof(PaginationResponse<Cinema>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCinemas(int page = 1)
        {
            PaginationResponse<Cinema> paginationResponse = await _cinemaService.GetAllCinemasAsync(page);
            return Ok(paginationResponse);
        }
        [HttpGet("{id:guid}", Name = "GetCinemaById")]
        [ProducesResponseType(typeof(Cinema), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCinemaById(Guid id)
        {
            Result<Cinema?> getCinemaResult = await _cinemaService.GetCinemaByIdAsync(id);
            if (getCinemaResult.IsFailed)
            {
                return Problem(getCinemaResult.Errors);
            }

            return Ok(getCinemaResult.Value);
        }
        [HttpPost]
        [ProducesResponseType(typeof(CinemaDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateCinema([FromForm] CinemaDTO cinemaDTO)
        {
            Result<Cinema> createCinemaResult = await _cinemaService.AddCinemaAsync(cinemaDTO);
            if (createCinemaResult.IsFailed)
            {
                return Problem(createCinemaResult.Errors);
            }

            return CreatedAtRoute(
                nameof(GetCinemaById),
                new { Id = createCinemaResult.Value.CinemaId },
                createCinemaResult.Value);
        }
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateCinema(Guid id, [FromForm] CinemaDTO cinemaDTO)
        {
            if (id != cinemaDTO.CinemaId)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Id not match.");
            }

            Result updateCinemaResult = await _cinemaService.UpdateCinemaAsync(cinemaDTO);
            if (updateCinemaResult.IsFailed)
            {
                return Problem(updateCinemaResult.Errors);
            }

            return NoContent();
        }
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> RemoveCinema(Guid id)
        {
            Result RemoveCinemaResult = await _cinemaService.RemoveCinemaAsync(id);
            if (RemoveCinemaResult.IsFailed)
            {
                return Problem(RemoveCinemaResult.Errors);
            }

            return NoContent();
        }

    }
}
