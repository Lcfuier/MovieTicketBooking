﻿using AutoMapper;
using MovieTicketBooking.Application.DTOs;
using MovieTicketBooking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cinema, CinemaDTO>().ForMember(dest => dest.MovieIds, opt => opt.MapFrom(
                src => src.Movies.Select(c => c.MovieId).ToArray())); ;
            CreateMap<CinemaDTO, Cinema>()
                .ForMember(dest=>dest.CinemaId,opt=>opt.MapFrom(src=>src.CinemaId))
                .ForMember(dest => dest.CinemaName, opt => opt.MapFrom(src => src.CinemaName));
            CreateMap<ShowTime, ShowTimeDTO>();
            CreateMap<ShowTimeDTO, ShowTime>();
        }
    } 
}
