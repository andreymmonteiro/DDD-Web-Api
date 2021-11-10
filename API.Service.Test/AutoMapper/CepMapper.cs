using Domain.Dtos.Cep;
using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static API.Service.Test.Users.BaseTestService;

namespace API.Service.Test.AutoMapper
{
    public class CepMapper
    {
        [Fact(DisplayName = "Can Map Cep.")]
        public async Task CAN_MAP_CEP()
        {
            var mapper = new AutoMapperFixture().GetMapper();
            CepEntity cepEntity = new CepEntity()
            {
                Cep = "95050812",
                Id = Guid.NewGuid(),
                Logradouro = Faker.Address.StreetName(),
                MunicipioId = Guid.NewGuid(),
                Numero = Faker.Address.UkPostCode(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow.AddHours(2)
            };
            var cepModel = mapper.Map<CepModel>(cepEntity);
            Assert.NotNull(cepModel);
            Assert.Equal(cepEntity.Id, cepModel.Id);
            Assert.Equal(cepEntity.Logradouro, cepModel.Logradouro);
            Assert.Equal(cepEntity.Numero, cepModel.Numero);
            Assert.Equal(cepEntity.Cep, cepModel.Cep);
            Assert.Equal(cepEntity.CreateAt, cepModel.CreateAt);
            Assert.Equal(cepEntity.UpdateAt, cepModel.UpdateAt);

            var cepDto = mapper.Map<CepDto>(cepModel);
            Assert.NotNull(cepDto);
            Assert.Equal(cepModel.Id, cepDto.Id);
            Assert.Equal(cepModel.Logradouro, cepDto.Logradouro);
            Assert.Equal(cepModel.Numero, cepDto.Numero);
            Assert.Equal(cepModel.Cep, cepDto.Cep);

            var cepDtoCreate = mapper.Map<CepDtoCreate>(cepModel);
            Assert.NotNull(cepDtoCreate);
            Assert.Equal(cepModel.Logradouro, cepDtoCreate.Logradouro);
            Assert.Equal(cepModel.Numero, cepDtoCreate.Numero);
            Assert.Equal(cepModel.Cep, cepDtoCreate.Cep);

            var cepDtoCreateResult = mapper.Map<CepDtoCreateResult>(cepEntity);
            Assert.NotNull(cepDtoCreateResult);
            Assert.Equal(cepEntity.Id, cepDtoCreateResult.Id);
            Assert.Equal(cepEntity.Logradouro, cepDtoCreateResult.Logradouro);
            Assert.Equal(cepEntity.Numero, cepDtoCreateResult.Numero);
            Assert.Equal(cepEntity.Cep, cepDtoCreateResult.Cep);
            Assert.Equal(cepEntity.CreateAt, cepDtoCreateResult.CreateAt);

            var cepDtoUpdate = mapper.Map<CepDtoUpdate>(cepModel);
            Assert.NotNull(cepDtoUpdate);
            Assert.Equal(cepModel.Id, cepDtoUpdate.Id);
            Assert.Equal(cepModel.Logradouro, cepDtoUpdate.Logradouro);
            Assert.Equal(cepModel.Numero, cepDtoUpdate.Numero);
            Assert.Equal(cepModel.Cep, cepDtoUpdate.Cep);

            var cepDtoUpdateResult = mapper.Map<CepDtoUpdateResult>(cepEntity);
            Assert.NotNull(cepDtoUpdateResult);
            Assert.Equal(cepEntity.Id, cepDtoUpdateResult.Id);
            Assert.Equal(cepEntity.Logradouro, cepDtoUpdateResult.Logradouro);
            Assert.Equal(cepEntity.Numero, cepDtoUpdateResult.Numero);
            Assert.Equal(cepEntity.Cep, cepDtoUpdateResult.Cep);
            Assert.Equal(cepEntity.UpdateAt, cepDtoUpdateResult.UpdateAt);

            var cepEntityMap = mapper.Map<CepEntity>(cepModel);
            Assert.NotNull(cepEntityMap);
            Assert.Equal(cepModel.Id, cepEntityMap.Id);
            Assert.Equal(cepModel.Logradouro, cepEntityMap.Logradouro);
            Assert.Equal(cepModel.Numero, cepEntityMap.Numero);
            Assert.Equal(cepModel.Cep, cepEntityMap.Cep);
            Assert.Equal(cepModel.CreateAt, cepEntityMap.CreateAt);
            Assert.Equal(cepModel.UpdateAt, cepEntityMap.UpdateAt);
        }
    }
}
