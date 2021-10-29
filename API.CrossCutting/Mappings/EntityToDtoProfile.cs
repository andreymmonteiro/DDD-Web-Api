using AutoMapper;
using Domain.Dtos.User;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<UserDto, UsersEntity>().ReverseMap();
            CreateMap<UserDtoCreateResult, UsersEntity>().ReverseMap();
            CreateMap<UserDtoUpdateResult, UsersEntity>().ReverseMap();
        }
    }
}
