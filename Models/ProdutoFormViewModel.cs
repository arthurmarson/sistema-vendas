using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using SistemaVenda.Entities;
using System.ComponentModel.DataAnnotations;

namespace SistemaVenda.Models
{
    public class ProdutoFormViewModel
    {
        public int? Codigo { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatório")]
        [StringLength(200, ErrorMessage = "A descrição pode ter no máximo 200 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatório")]
        public double Quantidade { get; set; }

        [Required(ErrorMessage = "Valor é obrigatório")]
        [Range(0.01, Double.PositiveInfinity, ErrorMessage = "Valor deve ser maior que zero")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal? Valor { get; set; }

        [Required(ErrorMessage = "Informe a categoria do produto")]
        public int? CodigoCategoria { get; set; }

        public ICollection<Categoria> Categorias { get; set; } = new List<Categoria>();


        public ProdutoFormViewModel()
        {
        }

        public ProdutoFormViewModel(Produto produto)
        {
            if (produto != null)
            {
                Codigo = produto.Codigo ?? 0;
                Descricao = produto.Descricao;
                Quantidade = produto.Quantidade;
                Valor = produto.Valor;
                CodigoCategoria = produto.CodigoCategoria;
            }
        }

        // Helper: converte viewmodel para entidade Cliente para salvar no service
        public Produto ToEntity()
        {
            return new Produto
            {
                Codigo = (int?)this.Codigo,
                Descricao = this.Descricao,
                Quantidade = this.Quantidade,
                Valor = (decimal)Valor,
                CodigoCategoria = this.CodigoCategoria ?? 0
            };
        }
    }


}
