using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage ="Email é campo obrigatório para o login")]
        [EmailAddress]
        [StringLength(100, ErrorMessage ="Ele deve ter no máximo {1} caracteres")]
        public string Email { get; set; }
    }
}
