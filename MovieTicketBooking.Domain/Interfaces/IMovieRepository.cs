﻿using MovieTicketBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Domain.Interfaces
{
    public interface IMovieRepository :IRepository<Movie>
    {
        void Update(Movie movie);
    }
}
