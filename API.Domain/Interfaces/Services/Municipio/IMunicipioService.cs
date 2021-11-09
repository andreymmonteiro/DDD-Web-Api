using Domain.Dtos.Municipio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services.Municipio
{
    public interface IMunicipioService
    {
        Task<MunicipioDto> Get(Guid Id);
        Task<MunicipioDtoComplete> GetComplete(Guid Id);
        Task<MunicipioDtoComplete> GetByIbge(int codigoIbge);
        Task<IEnumerable<MunicipioDto>> GetAll();
        Task<MunicipioDtoCreateResult> Post(MunicipioDtoCreate municipioDtoCreate);
        Task<MunicipioDtoUpdateResult> Put(MunicipioDtoUpdate municipioDtoUpdate);
        Task<bool> Delete(Guid Id);

    }
}
