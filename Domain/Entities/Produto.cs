using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVenda.Domain.Entities
{
    public class Produto : EntityBase
    {
        public string Descricao { get; set; }
        public double Quantidade { get; set; }
        public decimal Valor { get; set; }

        [ForeignKey("Categoria")]
        public int CodigoCategoria { get; set; }

        public Categoria? Categoria { get; set; }
        public ICollection<VendaProdutos> VendasP { get; set; }
    }
}
