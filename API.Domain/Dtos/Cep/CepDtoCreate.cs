using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Cep
{
    public class CepDtoCreate
    {
        [Required(ErrorMessage ="Name is required.")]
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        [Required(ErrorMessage = "CEP is required.")]
        public string Cep { get; set; }
        public Guid MunicipioId { get; set; }
    }
}
