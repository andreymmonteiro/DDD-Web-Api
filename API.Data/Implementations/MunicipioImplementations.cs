using Data.Context;
using Data.Repositories;
using Domain.Entities;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementations
{
    public class MunicipioImplementations : BaseRepository<MunicipioEntity>, IMunicipioRepository
    {
        private DbSet<MunicipioEntity> DataSet;
        public MunicipioImplementations(MyContext context) : base(context)
        {
            DataSet = context.Set<MunicipioEntity>();
        }

        public async Task<MunicipioEntity> GetComplete(Guid Id)
        {
            return await DataSet.AsNoTracking()
                                .Include(include => include.Uf)
                                .FirstOrDefaultAsync(first => first.Id.Equals(Id));
        }

        public async Task<MunicipioEntity> GetComplete(int codigoIbge)
        {
            return await DataSet.AsNoTracking()
                                .Include(include => include.Uf)
                                .FirstOrDefaultAsync(first => first.CodigoIbge.Equals(codigoIbge));
        }
    }
}
