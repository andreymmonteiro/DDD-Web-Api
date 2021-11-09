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
    public class UfMapper : AutoMapperFixture
    {
        private UfDto ufDto;
        private UfEntity ufEntity;
        private UfModel ufModel;

        [Fact(DisplayName ="Can Map Uf.")]
        public async Task CAN_MAP_UF()
        {

        }
        private void CreateObject() 
        {
            string name = "Espírito Santo";
            string sigla = "ES";
            Guid id = new Guid("c623f804-37d8-4a19-92c1-67fd162862e6");
            ufDto = new UfDto()
            {
                Id = id,
                Name = name,
                Sigla = sigla
            };
        }
    }
}
