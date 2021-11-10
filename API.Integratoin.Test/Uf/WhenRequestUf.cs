using Domain.Dtos.Uf;
using Domain.Entities;
using Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Integration.Test.Uf
{
    public class WhenRequestUf : BaseIntegration
    {
        [Fact(DisplayName ="Can Integration Uf.")]
        public async Task CAN_GET_UF() 
        {
            await AddToken();
            await Get();
        }
        private async Task Get() 
        {
            var resultHtttp = await GetDataAsync($"{hostApi}uf", client);
            var ufDtoString = await resultHtttp.Content.ReadAsStringAsync();
            var listUfDto = JsonConvert.DeserializeObject<List<UfDto>>(ufDtoString);
            Assert.Equal(HttpStatusCode.OK, resultHtttp.StatusCode);
            Assert.NotNull(listUfDto);
            var listUfEntity = mapper.Map<List<UfEntity>>(listUfDto);
            for (int i = 0; i < listUfDto.Count(); i++)
            {
                Test(listUfDto.OrderBy(order => order.Id).ToList()[i], listUfEntity.OrderBy(order => order.Id).ToList()[i]);
            }
        }
        private void Test(UfDto expected, UfEntity actual) 
        {
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Sigla, actual.Sigla);
        }
    }
}

