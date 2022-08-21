using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs;
using Core.models;

namespace Infraestructura.AutoMappers
{
    public class AutoMappers:Profile
    {
        public AutoMappers()
        {
            CreateMap<Cliente,ClienteDto>().ReverseMap();
            CreateMap<Cliente,ClientePostDto>().ReverseMap();
        }
    }
}