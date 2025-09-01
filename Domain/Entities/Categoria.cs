using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SistemaVenda.Domain.Entities
{
    public class Categoria : EntityBase
    {
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        public ICollection<Produto> Produtos { get; set; }

        public Categoria()
        {
        }
    }
}
