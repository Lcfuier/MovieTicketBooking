using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Infrastructure.Data
{
    public class MovieTicketBookingDbcontext : IdentityDbContext
    {
        public MovieTicketBookingDbcontext(DbContextOptions<MovieTicketBookingDbcontext> options) : base(options) { }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Cinema> Cinema{ get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<ShowTime> ShowTime{ get; set; }
        public DbSet<Customer> Customer { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cinema>(entity =>
            {
                entity.HasMany(d => d.Movies).WithMany(p => p.Cinemas)
                    .UsingEntity<Dictionary<string, object>>(
                        "CinemaMovie",
                        r => r.HasOne<Movie>().WithMany()
                            .HasForeignKey("MovieId")
                            .HasConstraintName("FK_CINEMAMOVIE_CINEMAMOVIE_MOVIES"),
                        l => l.HasOne<Cinema>().WithMany()
                            .HasForeignKey("CinemaId")
                            .HasConstraintName("FK_CINEMAMOVIE_CINEMAMOVIE_CINEMAS"),
                        j =>
                        {
                            j.HasKey("CinemaId", "MovieId").HasName("PK_CINEMAMOVIE");
                            j.ToTable("CinemaMovie");
                            j.HasIndex(new[] { "MovieId" }, "CINEMAMOVIE2_FK");
                            j.HasIndex(new[] { "CinemaId" }, "CINEMAMOVIE_FK");
                        });
            });
        }
        }
}
