using AutoMapper;
using Domain.Dtos.Municipio;
using Domain.Entities;
using Domain.Interfaces.Services.Municipio;
using Domain.Models;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class MunicipioService : IMunicipioService
    {
        private readonly IMunicipioRepository repository;
        private readonly IMapper mapper;

        public MunicipioService(IMunicipioRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<bool> Delete(Guid Id)
        {
            return await repository.DeleteAsync(Id);
        }

        public async Task<MunicipioDto> Get(Guid Id)
        {
            var resultEntity = await repository.SelectAsync(Id);
            return mapper.Map<MunicipioDto>(resultEntity);
        }

        public async Task<IEnumerable<MunicipioDto>> GetAll()
        {
            var resultEntities = await repository.SelectAsync();
            return mapper.Map<IEnumerable<MunicipioDto>>(resultEntities);
        }

        public async Task<MunicipioDtoComplete> GetByIbge(int codigoIbge)
        {
            var resultGet = await repository.GetComplete(codigoIbge);
            return mapper.Map<MunicipioDtoComplete>(resultGet);
        }

        public async Task<MunicipioDtoComplete> GetComplete(Guid Id)
        {
            var resultGet = await repository.GetComplete(Id);
            return mapper.Map<MunicipioDtoComplete>(resultGet);
        }

        public async Task<MunicipioDtoCreateResult> Post(MunicipioDtoCreate municipioDtoCreate)
        {
            var municipioModel = mapper.Map<MunicipioModel>(municipioDtoCreate);
            var resultPost = await repository.InsertAsync(mapper.Map<MunicipioEntity>(municipioModel));
            return mapper.Map<MunicipioDtoCreateResult>(resultPost);
        }

        public async Task<MunicipioDtoUpdateResult> Put(MunicipioDtoUpdate municipioDtoUpdate)
        {
            var municipioModel = mapper.Map<MunicipioModel>(municipioDtoUpdate);
            var resultPut = await repository.UpdateAsync(mapper.Map<MunicipioEntity>(municipioModel));
            return mapper.Map<MunicipioDtoUpdateResult>(resultPut);
        }
    }
}
