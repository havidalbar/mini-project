using System;
using AutoMapper;
using Core.Features.Queries.Authentications;
using Persistence.Models;

namespace Core.Features.Queries.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, PostLoginResponse>().ReverseMap();
        }
    }
}

