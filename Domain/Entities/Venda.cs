using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVenda.Domain.Entities
{
    public class Venda : EntityBase
    {
        public DateTime Data { get; set; }

        [ForeignKey("Cliente")]
        public int CodigoCliente { get; set; }
        public decimal Total { get; set; }
        public Cliente Cliente { get; set; }
        public ICollection<VendaProdutos> Produtos { get; set; }
    }
}
