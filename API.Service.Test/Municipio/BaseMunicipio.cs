using Domain.Dtos.Municipio;
using Domain.Interfaces.Services.Municipio;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Service.Test.Municipio
{
    public abstract class BaseMunicipio
    {
        public string name = Faker.Name.FullName();
        public Guid ufId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6");
        public int codigoIbge = Faker.RandomNumber.Next(1000000, 9999999);
        public Guid id = Guid.NewGuid();

        protected MunicipioDto municipio;
        protected MunicipioDtoCreate municipioDtoCreate;
        protected MunicipioDtoCreateResult municipioDtoCreateResult;
        protected MunicipioDtoUpdate municipioDtoUpdate;
        protected MunicipioDtoUpdateResult municipioDtoUpdateResult;
        protected MunicipioDtoComplete municipioDtoComplete;
        protected List<MunicipioDto> municipios { get; set; } = new List<MunicipioDto>();
        protected Mock<IMunicipioService> serviceMock;
        protected IMunicipioService service;
        private const int numericRandom = 10;
        protected BaseMunicipio()
        {
            AddToDto();
            AddToList();
        }
        
        private void AddToList() 
        {
            for (int i = 0; i < numericRandom; i++)
                municipios.Add(
                    new MunicipioDto()
                {
                    Name = Faker.Name.FullName(),
                    UfId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6"),
                    CodigoIbge = Faker.RandomNumber.Next(1000000, 9999999)
                });
        }
        private void AddToDto()
        {
            
            municipio = new MunicipioDto()
            {
                Id = id,
                Name = name,
                UfId = ufId,
                CodigoIbge = codigoIbge
            };
            municipioDtoCreate = new MunicipioDtoCreate()
            {
                Name = name,
                UfId = ufId,
                CodigoIbge = codigoIbge
            };
            municipioDtoCreateResult = new MunicipioDtoCreateResult()
            {
                Id = id,
                Name = name,
                UfId = ufId,
                CodigoIbge = codigoIbge
            };
            municipioDtoUpdate = new MunicipioDtoUpdate()
            {
                Id = id,
                Name = name,
                UfId = ufId,
                CodigoIbge = codigoIbge
            };
            municipioDtoUpdateResult = new MunicipioDtoUpdateResult()
            {
                Id = id,
                Name = name,
                UfId = ufId,
                CodigoIbge = codigoIbge
            };
            municipioDtoComplete = new MunicipioDtoComplete()
            {
                Id = id,
                Name = name,
                UfId = ufId,
                CodigoIbge = codigoIbge
            };
        }
    }
}
