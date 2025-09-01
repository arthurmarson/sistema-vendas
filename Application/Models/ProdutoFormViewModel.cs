using System.ComponentModel.DataAnnotations;

namespace SistemaVenda.Models
{
    public class ProdutoFormViewModel
    {
        public int Codigo { get; set; }

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

        public CategoriaViewModel? Categoria { get; set; }
        public IEnumerable<CategoriaViewModel>? Categorias { get; set; } = new List<CategoriaViewModel>();


    }


}
