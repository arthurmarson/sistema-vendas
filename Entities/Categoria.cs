using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;

namespace SistemaVenda.Entities
{
    public class Categoria
    {
        [Key]
        [Display(Name = "Código")]
        public int? Codigo { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();

        public Categoria()
        {
        }

        public Categoria(int? codigo, string descricao)
        {
            Codigo = codigo;
            Descricao = descricao;
        }
    }
}
