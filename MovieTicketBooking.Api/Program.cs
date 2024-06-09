using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Infrastructure.Data;
using AutoMapper;
using MovieTicketBooking.Api.Extensions;
using Microsoft.AspNetCore.Identity;
using MovieTicketBooking.Domain.Models;
using MovieTicketBooking.Application.Mapper;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;
});
builder.Services.AddAutoMapper(typeof(MappingProfile));
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MovieTicketBookingDbcontext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddLifetimeServices();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
