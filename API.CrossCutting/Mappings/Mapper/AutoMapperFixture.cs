using AutoMapper;
using CrossCutting.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Mapper
{
    public class AutoMapperFixture : IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IMapper GetMapper() 
        {
            var config = new AutoMapper.MapperConfiguration(map =>
            {
                map.AddProfile(new DtoToModelProfile());
                map.AddProfile(new EntityToDtoProfile());
                map.AddProfile(new ModelToEntityProfile());
            });
            return config.CreateMapper();
        }
    }
}
