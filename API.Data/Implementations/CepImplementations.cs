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
    public class CepImplementations : BaseRepository<CepEntity>, ICepRepository
    {
        private DbSet<CepEntity> DataSet;
        public CepImplementations(MyContext context) : base(context)
        {
            DataSet = context.Set<CepEntity>();
        }

        public async Task<CepEntity> SelectAsync(string cep)
        {
            return await DataSet.AsNoTracking()
                                .Include(include => include.Municipio)
                                .ThenInclude(include => include.Uf)
                                .FirstOrDefaultAsync(first => first.Cep.Equals(cep));
        }
    }
}
