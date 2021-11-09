using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Municipio
{
    public class MunicipioDtoCreate
    {
        [Required(ErrorMessage ="Name is required.")]
        [MaxLength(60, ErrorMessage ="Field Name have {1} Max Length.")]
        public string Name { get; set; }
        [Range(0, int.MaxValue, ErrorMessage ="IGBE is invalid.")]
        public int CodigoIbge { get; set; }
        [Required(ErrorMessage ="Field Uf is required.")]
        public Guid UfId { get; set; }
    }
}
