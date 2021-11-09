using Domain.Dtos.Cep;
using Domain.Interfaces.Services.Cep;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Service.Test.Cep
{
    public abstract class BaseCep
    {

        public string cep = "95050812";
        public Guid id = Guid.NewGuid();
        public string logradouro = Faker.Address.StreetName();
        public Guid municipioId = Guid.NewGuid();
        public string numero = Faker.Address.UkPostCode();
        protected List<CepDto> ceps { get; set; } = new List<CepDto>();
        protected CepDto cepDto;
        protected CepDtoCreate cepDtoCreate;
        protected CepDtoCreateResult cepDtoCreateResult;
        protected CepDtoUpdate cepDtoUpdate;    
        protected CepDtoUpdateResult cepDtoUpdateResult;
        protected Mock<ICepService> serviceMock;
        protected ICepService service;
        private const int numericRandom = 10;

        public BaseCep() 
        {
            AddToDto();
            AddToList();
        }
        private void AddToDto() 
        {
            

            cepDto = new CepDto()
            {
                Id = id,
                Logradouro = logradouro,
                MunicipioId = municipioId,
                Numero = numero,
                Cep = cep
            };
            cepDtoCreate = new CepDtoCreate() 
            {
                Logradouro = logradouro,
                MunicipioId = municipioId,
                Numero = numero,
                Cep = cep
            };
            cepDtoCreateResult = new CepDtoCreateResult()
            {
                Id = id,
                Logradouro = logradouro,
                MunicipioId = municipioId,
                Numero = numero,
                Cep = cep
            };
            cepDtoUpdate = new CepDtoUpdate() 
            {
                Id = id,
                Logradouro = logradouro,
                MunicipioId = municipioId,
                Numero = numero,
                Cep = cep
            };
            cepDtoUpdateResult = new CepDtoUpdateResult() 
            {
                Id = id,
                Logradouro = logradouro,
                MunicipioId = municipioId,
                Numero = numero,
                Cep = cep
            };

        }
        private void AddToList() 
        {
            for (int i = 0; i < numericRandom; i++)
                ceps.Add(new CepDto()
                {
                    Id = Guid.NewGuid(),
                    Logradouro = Faker.Address.StreetName(),
                    MunicipioId = Guid.NewGuid(),
                    Numero = Faker.Address.UkPostCode(),
                    Cep = Faker.RandomNumber.Next(10000000, 99999999).ToString()
                });
        }
    }
}
