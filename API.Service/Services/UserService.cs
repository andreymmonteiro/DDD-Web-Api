using AutoMapper;
using Domain.Dtos.User;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Services.Token;
using Domain.Interfaces.Services.Users;
using Domain.Models;
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
        private readonly IMapper mapper;        

        public UserService(IRepository<UserEntity> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await repository.DeleteAsync(id);
        }

        public async Task<UserDto> Get(Guid id)
        {
            var entity = await repository.SelectAsync(id);
            return mapper.Map<UserDto>(entity) ?? new UserDto();
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var entity  = await repository.SelectAsync();
            var resultUserDto = mapper.Map<IEnumerable<UserDto>>(entity);
            return resultUserDto;
        }

        public async Task<UserDtoCreateResult> Post(UserDtoCreate entity)
        {
            var model = mapper.Map<UserModel>(entity);
            var modelEntity = mapper.Map<UserEntity>(model);
            modelEntity = await repository.InsertAsync(modelEntity);
            return mapper.Map<UserDtoCreateResult>(modelEntity);
        }

        public async Task<UserDtoUpdateResult> Put(UserDtoUpdate entity)
        {
            var model = mapper.Map<UserModel>(entity);
            var modelEntity = mapper.Map<UserEntity>(model);
            modelEntity = await repository.UpdateAsync(modelEntity);
            return mapper.Map<UserDtoUpdateResult>(modelEntity);
        }
    }
}
