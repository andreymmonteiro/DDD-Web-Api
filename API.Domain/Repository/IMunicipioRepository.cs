using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IMunicipioRepository : IRepository<MunicipioEntity>
    {
        Task<MunicipioEntity> GetComplete(Guid Id);
        Task<MunicipioEntity> GetComplete(int codigoIbge);
    }
}
