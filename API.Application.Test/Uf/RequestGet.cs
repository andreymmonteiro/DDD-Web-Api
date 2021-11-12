using Domain.Dtos.Uf;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace API.Application.Test.Uf
{
    public class RequestGet : BaseUfRequest
    {

        [Fact]
        public async Task CAN_GET_UF() 
        {
            var ufDto = new UfDto() 
            {
                Id = id,
                Sigla = sigla,
                Name = name
            };
            serviceMock.Setup(setup => setup.Get(id)).ReturnsAsync(ufDto);
            InitializeController();
            var resultGet = await controller.Get(id);
            Assert.NotNull(resultGet);
            Assert.True(resultGet is OkObjectResult);
            var resultParse = (UfDto)((OkObjectResult)resultGet).Value;
            Assert.Equal(resultParse.Name, ufDto.Name);
            Assert.Equal(resultParse.Sigla, ufDto.Sigla);
            Assert.Equal(resultParse.Id, ufDto.Id);
        }
        private async Task GetAll() 
        {
            List<UfDto> ufDtos = GetDtos();
            serviceMock.Setup(setup => setup.GetAll()).ReturnsAsync(ufDtos);
            InitializeController();
            var resultGetAll = await controller.Get();
            Assert.NotNull(resultGetAll);
            Assert.True(resultGetAll is OkObjectResult);
            var resultListUfDtos = (List<UfDto>)((OkObjectResult)resultGetAll).Value;
            Assert.True(resultListUfDtos.Count() == 27);
        }
        private List<UfDto> GetDtos()
        {
            return new List<UfDto>()
            {
                new UfDto()
                {
                    Id = new Guid("22ffbd18-cdb9-45cc-97b0-51e97700bf71"),
                    Sigla = "AC",
                    Name = "Acre"
                },
                new UfDto()
                {
                    Id = new Guid("7cc33300-586e-4be8-9a4d-bd9f01ee9ad8"),
                    Sigla = "AL",
                    Name = "Alagoas"
                },
                new UfDto()
                {
                    Id = new Guid("cb9e6888-2094-45ee-bc44-37ced33c693a"),
                    Sigla = "AM",
                    Name = "Amazonas"
                },
                 new UfDto()
                 {
                     Id = new Guid("409b9043-88a4-4e86-9cca-ca1fb0d0d35b"),
                     Sigla = "AP",
                     Name = "Amapá"
                 },
                 new UfDto()
                 {
                     Id = new Guid("5abca453-d035-4766-a81b-9f73d683a54b"),
                     Sigla = "BA",
                     Name = "Bahia"
                 },
                 new UfDto()
                 {
                     Id = new Guid("5ff1b59e-11e7-414d-827e-609dc5f7e333"),
                     Sigla = "CE",
                     Name = "Ceará"
                 },
                 new UfDto()
                 {
                     Id = new Guid("bd08208b-bfca-47a4-9cd0-37e4e1fa5006"),
                     Sigla = "DF",
                     Name = "Distrito Federal"
                 },
                 new UfDto()
                 {
                     Id = new Guid("c623f804-37d8-4a19-92c1-67fd162862e6"),
                     Sigla = "ES",
                     Name = "Espírito Santo"
                 },
                 new UfDto()
                 {
                     Id = new Guid("837a64d3-c649-4172-a4e0-2b20d3c85224"),
                     Sigla = "GO",
                     Name = "Goiás"
                 },
                 new UfDto()
                 {
                     Id = new Guid("57a9e9f7-9aea-40fe-a783-65d4feb59fa8"),
                     Sigla = "MA",
                     Name = "Maranhão"
                 },
                 new UfDto()
                 {
                     Id = new Guid("27f7a92b-1979-4e1c-be9d-cd3bb73552a8"),
                     Sigla = "MG",
                     Name = "Minas Gerais"
                 },
                 new UfDto()
                 {
                     Id = new Guid("3739969c-fd8a-4411-9faa-3f718ca85e70"),
                     Sigla = "MS",
                     Name = "Mato Grosso do Sul"
                 },
                 new UfDto()
                 {
                     Id = new Guid("29eec4d3-b061-427d-894f-7f0fecc7f65f"),
                     Sigla = "MT",
                     Name = "Mato Grosso"
                 },
                 new UfDto()
                 {
                     Id = new Guid("8411e9bc-d3b2-4a9b-9d15-78633d64fc7c"),
                     Sigla = "PA",
                     Name = "Pará"
                 },
                 new UfDto()
                 {
                     Id = new Guid("1109ab04-a3a5-476e-bdce-6c3e2c2badee"),
                     Sigla = "PB",
                     Name = "Paraíba"
                 },
                 new UfDto()
                 {
                     Id = new Guid("ad5969bd-82dc-4e23-ace2-d8495935dd2e"),
                     Sigla = "PE",
                     Name = "Pernambuco"
                 },
                 new UfDto()
                 {
                     Id = new Guid("f85a6cd0-2237-46b1-a103-d3494ab27774"),
                     Sigla = "PI",
                     Name = "Piauí"
                 },
                 new UfDto()
                 {
                     Id = new Guid("1dd25850-6270-48f8-8b77-2f0f079480ab"),
                     Sigla = "PR",
                     Name = "Paraná"
                 },
                 new UfDto()
                 {
                     Id = new Guid("43a0f783-a042-4c46-8688-5dd4489d2ec7"),
                     Sigla = "RJ",
                     Name = "Rio de Janeiro"
                 },
                 new UfDto()
                 {
                     Id = new Guid("542668d1-50ba-4fca-bbc3-4b27af108ea3"),
                     Sigla = "RN",
                     Name = "Rio Grande do Norte"
                 },
                 new UfDto()
                 {
                     Id = new Guid("924e7250-7d39-4e8b-86bf-a8578cbf4002"),
                     Sigla = "RO",
                     Name = "Rondônia"
                 },
                 new UfDto()
                 {
                     Id = new Guid("9fd3c97a-dc68-4af5-bc65-694cca0f2869"),
                     Sigla = "RR",
                     Name = "Roraima"
                 },
                 new UfDto()
                 {
                     Id = new Guid("88970a32-3a2a-4a95-8a18-2087b65f59d1"),
                     Sigla = "RS",
                     Name = "Rio Grande do Sul"
                 },
                 new UfDto()
                 {
                     Id = new Guid("b81f95e0-f226-4afd-9763-290001637ed4"),
                     Sigla = "SC",
                     Name = "Santa Catarina"
                 },
                 new UfDto()
                 {
                     Id = new Guid("fe8ca516-034f-4249-bc5a-31c85ef220ea"),
                     Sigla = "SE",
                     Name = "Sergipe"
                 },
                 new UfDto()
                 {
                     Id = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6"),
                     Sigla = "SP",
                     Name = "São Paulo"
                 },
                 new UfDto()
                 {
                     Id = new Guid("971dcb34-86ea-4f92-989d-064f749e23c9"),
                     Sigla = "TO",
                     Name = "Tocantins"
                 }
            };
        }
    }
}
