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
    public class UserImplementation : BaseRepository<UserEntity>, IUserRepository
    {
        private DbSet<UserEntity> DataSet;
        public UserImplementation(MyContext context) : base(context)
        {
            this.DataSet = context.Set<UserEntity>();
        }
        public async Task<UserEntity> FindByLogin(string email)
        {
            return await DataSet.FirstOrDefaultAsync(user => user.Email.Equals(email));
        }
    }
}
