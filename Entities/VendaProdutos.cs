using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVenda.Entities
{
    public class VendaProdutos
    {
        public int CodigoVenda { get; set; }
        public int CodigoProduto { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
        public Produto Produto { get; set; }
        public Venda Venda { get; set; }
    }
}
