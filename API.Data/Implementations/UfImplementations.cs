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
    public class UfImplementations : BaseRepository<UfEntity>, IUfRepository
    {
        private DbSet<UfEntity> DataSet;
        public UfImplementations(MyContext context) : base(context)
        {
            DataSet = context.Set<UfEntity>();
        }
    }
}
