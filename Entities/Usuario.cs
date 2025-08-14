using System.ComponentModel.DataAnnotations;

namespace SistemaVenda.Entities
{
    public class Usuario
    {
        [Key]
        public int? Codigo { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }


    }
}
