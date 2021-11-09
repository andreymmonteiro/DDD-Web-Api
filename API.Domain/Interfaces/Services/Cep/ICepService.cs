using Domain.Dtos.Cep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services.Cep
{
    public interface ICepService
    {
        Task<CepDto> Get(Guid id);
        Task<CepDto> Get(string cep);
        Task<IEnumerable<CepDto>> GetAll();
        Task<CepDtoCreateResult> Post(CepDtoCreate cepDtoCreate);
        Task<CepDtoUpdateResult> Put(CepDtoUpdate cepDtoUpdate);
        Task<bool> Delete(Guid id);
    }
}
