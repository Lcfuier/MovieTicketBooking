using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MovieTicketBooking.Application.Interfaces;
using MovieTicketBooking.Application.Services;
using MovieTicketBooking.Domain.Interfaces;
using MovieTicketBooking.Infrastructure.UnitOfWork;
using System.Text;

namespace MovieTicketBooking.Api.Extensions
{
    public static class LifetimeServicesCollectionExtensions
    {
        public static IServiceCollection AddLifetimeServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddSingleton<IVnPayService, VnPayService>();
            services.AddAuth(configuration);
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICinemaService, CinemaService>();
            services.AddTransient<IShowTimeService, ShowTimeService>();
            services.AddTransient<ISeatService, SeatService>();
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IBookingDetailService, BookingDetailService>();
            services.AddTransient<IBookingService, BookingService>();
            return services;
        }
        private static IServiceCollection AddAuth(this IServiceCollection services,
       ConfigurationManager configuration)
        {

            // services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                    };
                });

            return services;
        }
    }
}
