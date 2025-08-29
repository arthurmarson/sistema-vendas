using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaVenda.Domain.Entities
{
    public class Cliente : EntityBase
    {
        public string Nome { get; set; }
        public string CNPJ_CPF { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public ICollection<Venda> Vendas { get; set; }
    }
}
