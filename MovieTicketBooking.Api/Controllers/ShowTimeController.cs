using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Application.Interfaces;

namespace MovieTicketBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShowTimeController : ApiController
    {
        private readonly IShowTimeService _cinemaService;
        private readonly IMapper _mapper;
        public IActionResult Index()
        {
            return View();
        }
    }
}
