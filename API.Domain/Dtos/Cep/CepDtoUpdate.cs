using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Cep
{
    public class CepDtoUpdate
    {
        [Required(ErrorMessage ="Id is Required.")]
        public Guid Id { get; set; }
        public string Numero { get; set; }
        public string Logradouro { get; set; }
        public string Cep { get; set; }
        public Guid MunicipioId { get; set; }
    }
}
