using AutoMapper;
using Domain.Dtos.Cep;
using Domain.Entities;
using Domain.Interfaces.Services.Cep;
using Domain.Models;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CepService : ICepService
    {
        private readonly ICepRepository repository;
        private readonly IMapper mapper;

        public CepService(ICepRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<bool> Delete(Guid id)
        {
            return await repository.DeleteAsync(id);
        }

        public async Task<CepDto> Get(Guid id)
        {
            var resultGet = await repository.SelectAsync(id);
            return mapper.Map<CepDto>(resultGet);
        }

        public async Task<CepDto> Get(string cep)
        {
            var resultGet = await repository.SelectAsync(cep);
            return mapper.Map<CepDto>(resultGet);
        }

        public async Task<IEnumerable<CepDto>> GetAll()
        {
            var resultGetAll = await repository.SelectAsync();
            return mapper.Map<IEnumerable<CepDto>>(resultGetAll);
        }

        public async Task<CepDtoCreateResult> Post(CepDtoCreate cepDtoCreate)
        {
            var cepModel = mapper.Map<CepModel>(cepDtoCreate);
            var resultPost = await repository.InsertAsync(mapper.Map<CepEntity>(cepModel));
            return mapper.Map<CepDtoCreateResult>(resultPost);
        }

        public async Task<CepDtoUpdateResult> Put(CepDtoUpdate cepDtoUpdate)
        {
            var cepModel = mapper.Map<CepModel>(cepDtoUpdate);
            var resultPut = await repository.UpdateAsync(mapper.Map<CepEntity>(cepModel));
            return mapper.Map<CepDtoUpdateResult>(resultPut);
        }
    }
}
