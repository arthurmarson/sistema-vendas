using SistemaVenda.Entities;
using System.ComponentModel.DataAnnotations;

namespace SistemaVenda.Models
{
    public class LoginFormViewModel
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a Senha")]
        public string Senha { get; set; }


    }
}
