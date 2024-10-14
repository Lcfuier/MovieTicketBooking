using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Infrastructure.Data;
using AutoMapper;
using MovieTicketBooking.Api.Extensions;
using Microsoft.AspNetCore.Identity;
using MovieTicketBooking.Domain.Models;
using MovieTicketBooking.Application.Mapper;
using MovieTicketBooking.Application.Common.JsonConverter;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MovieTicketBooking.Domain.Constants.MailSender;
using Microsoft.IdentityModel.Tokens;
using MovieTicketBooking.Application.Interfaces;
using MovieTicketBooking.Application.Services;
using System.Security.Claims;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        opt.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
        // other converter...
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date",
        Example = new OpenApiString(DateTime.Today.ToString("yyyy-MM-dd"))
    });
    opt.MapType<TimeOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "time",
        Example = new OpenApiString("00:00:00")
    });
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API ", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In=ParameterLocation.Header,
        Description="Please enter a valid token",
        Name="Authorization",
        Type=SecuritySchemeType.Http,
        Scheme="Bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string []{}
        }
    });
}
);;
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;
});
builder.Services.AddAutoMapper(typeof(MappingProfile));
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MovieTicketBookingDbcontext>(options =>
    options.UseSqlServer(connectionString));
   
builder.Services.AddIdentity<Customer, IdentityRole>(options => {
    options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
}).AddEntityFrameworkStores<MovieTicketBookingDbcontext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>options.SignIn.RequireConfirmedEmail=true);


//email Config

builder.Services.AddLifetimeServices(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
