using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Application.DTOs;
using MovieTicketBooking.Application.Interfaces;
using MovieTicketBooking.Domain.Models;
using MovieTicketBooking.Domain.Queries;

namespace MovieTicketBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ApiController
    {
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;
        public MovieController(IMovieService movieService, IMapper mapper)
        {
            _movieService= movieService;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(typeof(PaginationResponse<Movie>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllMovie(int page = 1)
        {
            PaginationResponse<Movie> paginationResponse = await _movieService.GetAllMoviesAsync(page);
            return Ok(paginationResponse);
        }
        [HttpGet("{id:guid}", Name = "GetMovieById")]
        [ProducesResponseType(typeof(Cinema), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMovieById(Guid id)
        {
            Result<Movie?> getMovieResult = await _movieService.GetMovieByIdAsync(id);
            if (getMovieResult.IsFailed)
            {
                return Problem(getMovieResult.Errors);
            }

            return Ok(getMovieResult.Value);
        }
        [HttpPost]
        [ProducesResponseType(typeof(MovieDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateMovie([FromForm] MovieDTO movieDTO)
        {
            Result<Movie> createMovieResult = await _movieService.AddMovieAsync(movieDTO);
            if (createMovieResult.IsFailed)
            {
                return Problem(createMovieResult.Errors);
            }

            return CreatedAtRoute(
                nameof(GetMovieById),
                new { Id = createMovieResult.Value.MovieId },
                createMovieResult.Value);
        }
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateMovie(Guid id, [FromForm] MovieDTO movieDTO)
        {
            if (id != movieDTO.MovieId)
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Id not match.");
            }

            Result updateMovieResult = await _movieService.UpdateMovieAsync(movieDTO);
            if (updateMovieResult.IsFailed)
            {
                return Problem(updateMovieResult.Errors);
            }

            return NoContent();
        }
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> RemoveMovie(Guid id)
        {
            Result RemoveMovieResult = await _movieService.RemoveMovieAsync(id);
            if (RemoveMovieResult.IsFailed)
            {
                return Problem(RemoveMovieResult.Errors);
            }

            return NoContent();
        }

    }
}
