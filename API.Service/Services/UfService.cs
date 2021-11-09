using AutoMapper;
using Domain.Dtos.Uf;
using Domain.Interfaces.Services.Uf;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UfService : IUfService
    {
        private readonly IUfRepository repository;
        private readonly IMapper mapper;

        public UfService(IUfRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<UfDto> Get(Guid Id)
        {
            var resultEntity = await repository.SelectAsync(Id);
            return mapper.Map<UfDto>(resultEntity);
        }

        public async Task<IEnumerable<UfDto>> GetAll()
        {
            var resultEntity = await repository.SelectAsync();
            return mapper.Map<IEnumerable<UfDto>>(resultEntity);
        }
    }
}
