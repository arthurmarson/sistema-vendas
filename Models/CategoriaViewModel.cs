using System.ComponentModel.DataAnnotations;

namespace SistemaVenda.Models
{
    public class CategoriaViewModel
    {
        public int Codigo { get; set; }

        [Required(ErrorMessage = "Informe a descrição da categoria!")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}
