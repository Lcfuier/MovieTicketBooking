using MovieTicketBooking.Application.Interfaces;
using MovieTicketBooking.Application.Services;
using MovieTicketBooking.Domain.Interfaces;
using MovieTicketBooking.Infrastructure.UnitOfWork;

namespace MovieTicketBooking.Api.Extensions
{
    public static class LifetimeServicesCollectionExtensions
    {
        public static IServiceCollection AddLifetimeServices(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICinemaService, CinemaService>();
            services.AddTransient<IShowTimeService, ShowTimeService>();
            
            return services;
        }
    }
}
