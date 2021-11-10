using AutoMapper;
using Domain.Dtos.Municipio;
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
    public class MunicipioMapper
    {
        private IMapper mapper;
        [Fact(DisplayName = "Can Map Municipio.")]
        public async Task CAN_MAP_MUNICIPIO()
        {
            MunicipioEntity municipioEntity = new MunicipioEntity()
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                UfId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6"),
                CodigoIbge = Faker.RandomNumber.Next(1000000, 9999999),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow.AddHours(2)
            };
            mapper = new AutoMapperFixture().GetMapper();
            var municipioModel = mapper.Map<MunicipioModel>(municipioEntity);
            Assert.NotNull(municipioModel);
            Assert.Equal(municipioEntity.Id, municipioModel.Id);
            Assert.Equal(municipioEntity.Name, municipioModel.Name);
            Assert.Equal(municipioEntity.UfId, municipioModel.UfId);
            Assert.Equal(municipioEntity.CodigoIbge, municipioModel.CodigoIbge);
            Assert.Equal(municipioEntity.CreateAt, municipioModel.CreateAt);
            Assert.Equal(municipioEntity.UpdateAt, municipioModel.UpdateAt);

            var municipioDto = mapper.Map<MunicipioModel>(municipioModel);
            Assert.NotNull(municipioDto);
            Assert.Equal(municipioModel.Id, municipioDto.Id);
            Assert.Equal(municipioModel.Name, municipioDto.Name);
            Assert.Equal(municipioModel.UfId, municipioDto.UfId);
            Assert.Equal(municipioModel.CodigoIbge, municipioDto.CodigoIbge);
            Assert.Equal(municipioModel.CreateAt, municipioDto.CreateAt);
            Assert.Equal(municipioModel.UpdateAt, municipioDto.UpdateAt);

            var municipioDtoCreate = mapper.Map<MunicipioDtoCreate>(municipioModel);
            Assert.NotNull(municipioDtoCreate);
            Assert.Equal(municipioModel.Name, municipioDtoCreate.Name);
            Assert.Equal(municipioModel.UfId, municipioDtoCreate.UfId);
            Assert.Equal(municipioModel.CodigoIbge, municipioDtoCreate.CodigoIbge);

            var municipioDtoCreateResult = mapper.Map<MunicipioDtoCreateResult>(municipioEntity);
            Assert.NotNull(municipioDtoCreateResult);
            Assert.Equal(municipioEntity.Id, municipioDtoCreateResult.Id);
            Assert.Equal(municipioEntity.Name, municipioDtoCreateResult.Name);
            Assert.Equal(municipioEntity.UfId, municipioDtoCreateResult.UfId);
            Assert.Equal(municipioEntity.CodigoIbge, municipioDtoCreateResult.CodigoIbge);
            Assert.Equal(municipioEntity.CreateAt, municipioDtoCreateResult.CreateAt);

            var municipioUpdate = mapper.Map<MunicipioDtoUpdate>(municipioModel);
            Assert.NotNull(municipioUpdate);
            Assert.Equal(municipioModel.Id, municipioUpdate.Id);
            Assert.Equal(municipioModel.Name, municipioUpdate.Name);
            Assert.Equal(municipioModel.UfId, municipioUpdate.UfId);
            Assert.Equal(municipioModel.CodigoIbge, municipioUpdate.CodigoIbge);

            var municipioUpdateResult = mapper.Map<MunicipioDtoUpdateResult>(municipioEntity);
            Assert.NotNull(municipioUpdateResult);
            Assert.Equal(municipioEntity.Id, municipioUpdateResult.Id);
            Assert.Equal(municipioEntity.Name, municipioUpdateResult.Name);
            Assert.Equal(municipioEntity.UfId, municipioUpdateResult.UfId);
            Assert.Equal(municipioEntity.CodigoIbge, municipioUpdateResult.CodigoIbge);
            Assert.Equal(municipioEntity.UpdateAt, municipioUpdateResult.UpdateAt);

            var municipioDtoComplete = mapper.Map<MunicipioDtoComplete>(municipioEntity);
            Assert.NotNull(municipioDtoComplete);
            Assert.Equal(municipioEntity.Id, municipioDtoComplete.Id);
            Assert.Equal(municipioEntity.Name, municipioDtoComplete.Name);
            Assert.Equal(municipioEntity.UfId, municipioDtoComplete.UfId);
            Assert.Equal(municipioEntity.CodigoIbge, municipioDtoComplete.CodigoIbge);

            var municipioEntityMap = mapper.Map<MunicipioEntity>(municipioModel);
            Assert.NotNull(municipioEntityMap);
            Assert.Equal(municipioModel.Id, municipioEntityMap.Id);
            Assert.Equal(municipioModel.Name, municipioEntityMap.Name);
            Assert.Equal(municipioModel.UfId, municipioEntityMap.UfId);
            Assert.Equal(municipioModel.CodigoIbge, municipioEntityMap.CodigoIbge);
            Assert.Equal(municipioModel.CreateAt, municipioEntityMap.CreateAt);
            Assert.Equal(municipioModel.UpdateAt, municipioEntityMap.UpdateAt);
        }
    }
}
