using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UserService : IUsersService
    {
        private readonly IRepository<UserEntity> repository;

        public UserService(IRepository<UserEntity> repository)
        {
            this.repository = repository;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await repository.DeleteAsync(id);
        }

        public async Task<UserEntity> Get(Guid id)
        {
            return await repository.SelectAsync(id);
        }

        public async Task<IEnumerable<UserEntity>> GetAll()
        {
            return await repository.SelectAsync();
        }

        public async Task<UserEntity> Post(UserEntity entity)
        {
            return await repository.InsertAsync(entity);
        }

        public async Task<UserEntity> Put(UserEntity entity)
        {
            return await repository.UpdateAsync(entity);
        }
    }
}
