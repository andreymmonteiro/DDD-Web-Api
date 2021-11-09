using API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MunicipioEntity : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public int CodigoIbge { get; set; }
        [Required]
        public Guid UfId { get; set; }
        public UfEntity Uf { get; set; }
        public IEnumerable<CepEntity> Ceps { get; set; }
    }
}
