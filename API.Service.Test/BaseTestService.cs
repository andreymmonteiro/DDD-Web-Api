using AutoMapper;
using CrossCutting.Mappings;
using System;
using Xunit;

namespace API.Service.Test.Users
{
    public abstract class BaseTestService
    {
        public IMapper mapper { get; set; }

        protected BaseTestService()
        {
            mapper = new AutoMapperFixture().GetMapper();
        }

        public class AutoMapperFixture : IDisposable
        {
            public IMapper GetMapper() 
            {
                var configuration = new MapperConfiguration(config =>
                                {
                                    config.AddProfile(new ModelToEntityProfile());
                                    config.AddProfile(new DtoToModelProfile());
                                    config.AddProfile(new EntityToDtoProfile());
                                });
                return configuration.CreateMapper();
            }
            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }
    }
}
