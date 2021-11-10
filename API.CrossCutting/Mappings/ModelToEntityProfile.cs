using AutoMapper;
using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Mappings
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile()
        {
            UserProfile();
            UfProfile();
            MunicipioProfile();
            CepProfile();
        }
        private void UserProfile() 
        {
            CreateMap<UsersEntity, UserModel>().ReverseMap();
        }
        private void UfProfile() 
        {
            CreateMap<UfEntity, UfModel>().ReverseMap();
        }
        private void MunicipioProfile() 
        {
            CreateMap<MunicipioEntity, MunicipioModel>().ReverseMap();
        }
        private void CepProfile() 
        {
            CreateMap<CepModel, CepEntity>().ReverseMap();
        }
    }
}
