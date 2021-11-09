using API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UfEntity : BaseEntity
    {
        [Required]
        [MaxLength(2)]
        public string Sigla { get;set; }   
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public IEnumerable<MunicipioEntity> Municipios { get; set; }

    }
}
