using AutoMapper;
using Domain.Dtos.Uf;
using Domain.Entities;
using Domain.Models;
using Service.Services.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Service.Test.AutoMapper
{
    public class UfMapper
    {
        private UfEntity ufEntity;
        private IMapper mapper;

        [Fact(DisplayName ="Can Map Uf.")]
        public async Task CAN_MAP_UF()
        {
            CreateObject();
            mapper = new AutoMapperFixture().GetMapper();
            var ufModel = mapper.Map<UfModel>(ufEntity);

            Assert.NotNull(ufModel);
            Assert.Equal(ufEntity.Id, ufModel.Id);
            Assert.Equal(ufEntity.Name, ufModel.Name);
            Assert.Equal(ufEntity.CreateAt, ufModel.CreateAt);
            Assert.Equal(ufEntity.Sigla, ufModel.Sigla);

            var ufDto = mapper.Map<UfDto>(ufModel);
            Assert.NotNull(ufDto);
            Assert.Equal(ufModel.Id, ufDto.Id);
            Assert.Equal(ufModel.Name, ufDto.Name);
            Assert.Equal(ufModel.Sigla, ufDto.Sigla);

        }
        
        private void CreateObject() 
        {
            string name = "Espírito Santo";
            string sigla = "ES";
            Guid id = new Guid("c623f804-37d8-4a19-92c1-67fd162862e6");
            ufEntity = new UfEntity()
            {
                Id = id,
                Name = name,
                Sigla = sigla,
                CreateAt= DateTime.Now,
                UpdateAt = DateTime.Now.AddHours(2)
            };
        }
    }
}


