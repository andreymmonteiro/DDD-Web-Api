using Domain.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Service.Test.Users
{
    public class UsersTest
    {
        public static string NameUser { get; set; }
        public static string EmailUser { get; set; }

        public static string AlterNameUser { get; set; }
        public static string AlterEmailUser { get; set; }
        public static Guid IdUser { get; set; }
        public List<UserDto> ListUserDto = new List<UserDto>();
        public UserDto userDto;
        public UserDtoCreate userCreateDto;
        public UserDtoCreateResult userDtoCreateResult;
        public UserDtoUpdate userDtoUpdate;
        public UserDtoUpdateResult userDtoUpdateResult;

        public UsersTest() 
        {
            IdUser = Guid.NewGuid();
            NameUser = Faker.Name.FullName();
            EmailUser = Faker.Internet.Email();
            AlterEmailUser = Faker.Internet.Email();
            AlterNameUser = Faker.Name.FullName();

            for(int i=0; i < 10; i++) 
            {
                ListUserDto.Add(new UserDto() 
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email()
                });
            }
            userDto = new UserDto()
            {
                Id = IdUser,
                Name = NameUser,
                Email = EmailUser
            };
            userCreateDto = new UserDtoCreate()
            {
                Name = NameUser,
                Email = EmailUser
            };
            userDtoCreateResult = new UserDtoCreateResult() 
            {
                Id = IdUser,
                Name = NameUser,
                Email = EmailUser,
                CreateAt = DateTime.UtcNow
            };
            userDtoUpdate = new UserDtoUpdate()
            {
                Name = AlterNameUser,
                Email = AlterEmailUser
            };
            userDtoUpdateResult = new UserDtoUpdateResult()
            {
                Name = AlterNameUser,
                Email = AlterEmailUser,
                Id = IdUser,
                UpdateAt = DateTime.UtcNow
            };
        }

    }
}
