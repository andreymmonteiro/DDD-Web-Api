using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.User
{
    public class UserDtoCreate
    {
        [Required(ErrorMessage = "Este é um campo obrigatório")]
        [StringLength(60, ErrorMessage = "Este campo deve possuir no máximo {1} caracteres")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Este é um campo obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail informado inválido")]
        [StringLength(100, ErrorMessage = "Este campo deve possuir no máximo {1} caracteres")]
        public string Email { get; set; }
    }
}
