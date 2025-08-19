using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using SistemaVenda.Entities;
using System.ComponentModel.DataAnnotations;

namespace SistemaVenda.Models
{
    public class ClienteFormViewModel
    {
        public ClienteFormViewModel()
        {
        }

        public ClienteFormViewModel(Cliente cliente)
        {
            if (cliente != null)
            {
                Codigo = cliente.Codigo ?? 0;
                Nome = cliente.Nome;
                CNPJ_CPF = cliente.CNPJ_CPF;
                Email = cliente.Email;
                Celular = cliente.Celular;
            }
        }

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

        // Helper: converte viewmodel para entidade Cliente para salvar no service
        public Cliente ToEntity()
        {
            return new Cliente
            {
                Codigo = (int?)this.Codigo, 
                Nome = this.Nome,
                CNPJ_CPF = this.CNPJ_CPF,
                Email = this.Email,
                Celular = this.Celular
            };
        }
    }


}
