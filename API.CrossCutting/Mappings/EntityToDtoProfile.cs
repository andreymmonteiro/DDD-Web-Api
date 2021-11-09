using AutoMapper;
using Domain.Dtos.Cep;
using Domain.Dtos.Municipio;
using Domain.Dtos.Uf;
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
            UserProfile();
            UfProfile();
            MunicipioProfile();
            CepProfile();
        }
        private void UserProfile() 
        {
            CreateMap<UserDto, UsersEntity>().ReverseMap();
            CreateMap<UserDtoCreateResult, UsersEntity>().ReverseMap();
            CreateMap<UserDtoUpdateResult, UsersEntity>().ReverseMap();
        }
        private void UfProfile() 
        {
            CreateMap<UfDto, UfEntity>().ReverseMap();
        }
        private void MunicipioProfile() 
        {
            CreateMap<MunicipioDto, MunicipioEntity>().ReverseMap();
            CreateMap<MunicipioDtoCreateResult, MunicipioEntity>().ReverseMap();
            CreateMap<MunicipioDtoUpdateResult, MunicipioEntity>().ReverseMap();
            CreateMap<MunicipioDtoComplete, MunicipioEntity>().ReverseMap();
        }
        private void CepProfile() 
        {
            CreateMap<CepDto, CepEntity>().ReverseMap();
            CreateMap<CepDtoCreateResult, CepEntity>().ReverseMap();
            CreateMap<CepDtoUpdateResult, CepEntity>().ReverseMap();
        }
    }
}
