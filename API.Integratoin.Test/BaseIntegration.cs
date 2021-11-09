using API.Integration.Test.Dto;
using application;
using AutoMapper;
using Data.Context;
using Domain.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Service.Services.Mapper;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Integration.Test
{
    public class BaseIntegration : IDisposable
    {
        public MyContext myContext { get; private set; }
        public HttpClient client { get; private set;  } 
        public IMapper mapper { get; set; }
        public string hostApi { get; set; }
        public HttpResponseMessage response { get; set; }
        protected const string emailUserMigration = "baki@hanma.com";
        protected const int numericRandon = 5;

        public BaseIntegration()
        {
            hostApi = "http://localhost:5000/api/";
            var builder = new WebHostBuilder()
                                .UseEnvironment("Testing")
                                .UseStartup<Startup>();
            var server = new TestServer(builder);
            myContext = server.Host.Services.GetService(typeof(MyContext)) as MyContext;
            myContext.Database.Migrate();
            mapper = new AutoMapperFixture().GetMapper();
            client = server.CreateClient();
        }

        public void Dispose()
        {
            
            myContext.Database.EnsureDeleted();
            myContext.Dispose();
            client.Dispose();

        }
        public async Task AddToken()
        {
            var loginDto = new LoginDto()
            {
                Email = emailUserMigration
            };
            var resultLogin = await PostDataAsync(loginDto, $"{hostApi}login", client);
            var jsonLogin = await resultLogin.Content.ReadAsStringAsync();
            var loginObject = JsonConvert.DeserializeObject<LoginResponseDto>(jsonLogin);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", loginObject.AcessToken);
        }
        public static async Task<HttpResponseMessage> PostDataAsync(object classData, string url, HttpClient client)
        {
            return await client.PostAsync(url, 
                new StringContent(JsonConvert.SerializeObject(classData), System.Text.Encoding.UTF8,"application/json"));
        }
        public static async Task<HttpResponseMessage> GetDataAsync(string url, HttpClient client)
        {
            return await client.GetAsync(url);
        }
        public static async Task<HttpResponseMessage> PutDataAsync(string url, object classObject, HttpClient client) 
        {
            return await client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(classObject),
                System.Text.Encoding.UTF8, "application/json"));
        }
        public static async Task<HttpResponseMessage> DeleteDataAsync(string url,HttpClient client) 
        {
            return await client.DeleteAsync(url);
        }
    }
}
