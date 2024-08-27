using System;
using AutoMapper;
using Core.Features.Queries.Authentications;
using Persistence.Models;

namespace Core.Features.Queries.Mappers
{
    public class RoleMapper : Profile
    {
        public RoleMapper()
        {
            CreateMap<Role, RoleLoginResponse>().ReverseMap();
        }
    }
}

