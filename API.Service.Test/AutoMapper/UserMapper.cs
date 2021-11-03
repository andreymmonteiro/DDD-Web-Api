using API.Service.Test.Users;
using Domain.Dtos.User;
using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Service.Test.AutoMapper
{
    public class UserMapper : BaseTestService
    {
        [Fact(DisplayName ="Can map models.")]
        public void CAN_MAP() 
        {
            var model = new UserModel()
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email =Faker.Internet.Email(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };

            var entity = mapper.Map<UsersEntity>(model);
            Assert.Equal(entity.Name, model.Name);
            Assert.Equal(entity.Email, model.Email);
            Assert.Equal(entity.CreateAt, model.CreateAt);
            Assert.Equal(entity.UpdateAt, model.UpdateAt);

            var userDto = mapper.Map<UserDto>(model);

            Assert.Equal(userDto.Name, model.Name);
            Assert.Equal(userDto.Email, model.Email);
            Assert.Equal(userDto.Id, model.Id);

            var listEntity = new List<UsersEntity>();
            for(int i =0; i < 5; i++) 
            {
                listEntity.Add(new UsersEntity()
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow

                });
            }
            var listDto = mapper.Map<List<UserDto>>(listEntity);
            Assert.True(listDto.Count() == listEntity.Count());
            for(int i=0; i < listDto.Count(); i++) 
            {
                Assert.Equal(listDto[i].Id, listEntity[i].Id);
                Assert.Equal(listDto[i].Name, listEntity[i].Name);
                Assert.Equal(listDto[i].Email, listEntity[i].Email);
            }
            var userDtoCreateResult = mapper.Map<UserDtoCreateResult>(entity);
            Assert.Equal(userDtoCreateResult.Name, entity.Name);
            Assert.Equal(userDtoCreateResult.Email, entity.Email);
            Assert.Equal(userDtoCreateResult.CreateAt, entity.CreateAt);

            var userDtoUpdateResult = mapper.Map<UserDtoUpdateResult>(entity);
            Assert.Equal(userDtoUpdateResult.Name, entity.Name);
            Assert.Equal(userDtoUpdateResult.Email, entity.Email);
            Assert.Equal(userDtoUpdateResult.UpdateAt, entity.UpdateAt);

            var userModel = mapper.Map<UserModel>(userDto);
            Assert.Equal(userModel.Name, userDto.Name);
            Assert.Equal(userModel.Email, userDto.Email);

            var userCreate = mapper.Map<UserDtoCreate>(userModel);
            Assert.Equal(userCreate.Name, userModel.Name);
            Assert.Equal(userCreate.Email, userModel.Email);
            
            
            var userUpdate = mapper.Map<UserDtoUpdate>(userModel);
            Assert.Equal(userUpdate.Name, userModel.Name);
            Assert.Equal(userUpdate.Email, userModel.Email);
            Assert.Equal(userUpdate.Id, userModel.Id);
        }
    }
}
