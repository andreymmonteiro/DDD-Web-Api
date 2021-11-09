using API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CepEntity : BaseEntity
    {
        [Required]
        public string Cep { get; set; }
        [MaxLength(60)]
        public string Logradouro { get; set; }
        [MaxLength(10)]
        public string Numero { get; set; }
        [Required]
        public Guid MunicipioId { get; set; }
        public MunicipioEntity Municipio { get; set; }
    }
}
