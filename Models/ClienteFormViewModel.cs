using System.ComponentModel.DataAnnotations;

namespace SistemaVenda.Models
{
    public class ClienteFormViewModel
    {
        public int Codigo { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(150, ErrorMessage = "O nome pode ter no máximo 150 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "CNPJ/CPF é obrigatório")]
        [StringLength(20, ErrorMessage = "CNPJ/CPF inválido")]
        public string CNPJ_CPF { get; set; }

        [Required(ErrorMessage = "E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [StringLength(150)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Celular é obrigatório")]
        [StringLength(20)]
        public string Celular { get; set; }

    }

}
