using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SistemaVenda.Domain.Entities
{
    public class Usuario : EntityBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }


    }
}
