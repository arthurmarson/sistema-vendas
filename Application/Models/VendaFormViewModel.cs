using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SistemaVenda.Models
{
    public class VendaFormViewModel
    {
        public int? Codigo { get; set; }

        [Required(ErrorMessage = "Data é obrigatória")]
        [Display(Name = "Data da Venda")]
        public DateTime Data { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Cliente é obrigatório")]
        public int? CodigoCliente { get; set; }

        [Display(Name = "Total da Venda")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total deve ser maior que zero")]
        public decimal Total { get; set; }


        [Display(Name = "Lista de Clientes")]
        public IEnumerable<SelectListItem> ListaClientes { get; set; } = new List<SelectListItem>();

        [Display(Name = "Lista de Produtos")]
        public IEnumerable<SelectListItem> ListaProdutos { get; set; } = new List<SelectListItem>();

        public string? JsonProdutos { get; set; }


        public string? NomeCliente => ListaClientes
            .FirstOrDefault(c => c.Value == CodigoCliente?.ToString())?.Text;
    }
}
