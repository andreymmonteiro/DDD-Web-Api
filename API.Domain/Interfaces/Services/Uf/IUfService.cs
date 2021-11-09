using Domain.Dtos.Uf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services.Uf
{
    public interface IUfService
    {
        Task<UfDto> Get(Guid Id);
        Task<IEnumerable<UfDto>> GetAll();
    }
}
