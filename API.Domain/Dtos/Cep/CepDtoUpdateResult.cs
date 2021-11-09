using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Cep
{
    public class CepDtoUpdateResult
    {
        public Guid Id { get; set; }
        public string Numero { get; set; }
        public string Logradouro { get; set; }
        public string Cep { get; set; }
        public Guid MunicipioId { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
