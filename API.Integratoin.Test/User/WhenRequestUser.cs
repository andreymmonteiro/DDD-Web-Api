using Domain.Dtos.User;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Integration.Test.User
{
    public class WhenRequestUser : BaseIntegration
    {
        private List<UserDtoCreate> usersDtoCreate { get; set; } = new List<UserDtoCreate>();
        private Random random = new Random();

        [Fact(DisplayName ="Can execute Post.")]
        public async Task CAN_POST() 
        {
            await AddToken();
            int countUsers = random.Next(numericRandon) + 1;
            for(int i=0; i <= countUsers; i++) 
            {
                usersDtoCreate.Add(new UserDtoCreate()
                {
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email()
                });
                response = await PostDataAsync(usersDtoCreate[i], $"{hostApi}users", client);
                var resultPost = await response.Content.ReadAsStringAsync();
                var resultObjetc = JsonConvert.DeserializeObject<UserDtoCreateResult>(resultPost);
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
                Assert.Equal(resultObjetc.Name, usersDtoCreate[i].Name);
                Assert.Equal(resultObjetc.Email, usersDtoCreate[i].Email);
                Assert.False(resultObjetc.Id == default(Guid));
            }
            
            var userModel = await GetAll();
            var userDtoUpdate = mapper.Map<UserDtoUpdate>(userModel[random.Next(countUsers)]);
            await Put(userDtoUpdate);

        }

        private async Task<List<UserModel>> GetAll() 
        {
            response = await GetDataAsync($"{hostApi}users", client);
            var resultHttpResponse = await response.Content.ReadAsStringAsync();
            var resultObject = JsonConvert.DeserializeObject<List<UserDto>>(resultHttpResponse);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(resultObject);
            Assert.True(resultObject.Count() > 0);

            var userMigration = resultObject.Find(find => find.Email == emailUserMigration);
            Assert.True(resultObject.Remove(userMigration));
            
            for (int i=0; i < resultObject.Count(); i++) 
            {
                Assert.Equal(usersDtoCreate.OrderBy(order =>order.Name).ToList()[i].Name, resultObject.OrderBy(order => order.Name).ToList()[i].Name);
                Assert.Equal(usersDtoCreate.OrderBy(order => order.Name).ToList()[i].Email, resultObject.OrderBy(order => order.Name).ToList()[i].Email);
                await GetById(resultObject[i]);
            }
            List<UserModel> usersModel = mapper.Map<List<UserModel>>(resultObject);
            return usersModel;
        }
        private async Task GetById(UserDto user) 
        {
            response = await GetDataAsync($"{hostApi}users/{user.Id}", client);
            var resultHttpResponse = await response.Content.ReadAsStringAsync();
            var resultObject = JsonConvert.DeserializeObject<UserDto>(resultHttpResponse);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(resultObject);
            Assert.Equal(user.Id, resultObject.Id); 
            Assert.Equal(user.Name, resultObject.Name);
            Assert.Equal(user.Email, resultObject.Email);
        }
        private async Task Put(UserDtoUpdate user) 
        {
            user.Name = "Um nome teste randomico";
            response = await PutDataAsync($"{hostApi}users", user, client);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var resultObject = JsonConvert.DeserializeObject<UserDtoUpdateResult>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(resultObject);
            Assert.Equal(user.Name, resultObject.Name);
            Assert.Equal(user.Email, resultObject.Email);
            Assert.Equal(user.Id, resultObject.Id);
        }
    }
}
