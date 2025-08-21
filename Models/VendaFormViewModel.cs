using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVenda.Entities;
using System.ComponentModel.DataAnnotations;

namespace SistemaVenda.Models
{
    public class VendaFormViewModel
    {
        public int? Codigo { get; set; }

        [Required(ErrorMessage = "Data é obrigatória")]
        [Display(Name = "Data da Venda")]
        public DateTime? Data { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Cliente é obrigatório")]
        public int? CodigoCliente { get; set; }

        [Display(Name = "Total da Venda")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total deve ser maior que zero")]
        public decimal Total { get; set; }


        // ATRIBUTOS DE RELACIONAMENTO
        [Display(Name = "Lista de Clientes")]
        public IEnumerable<SelectListItem> ListaClientes { get; set; }

        [Display(Name = "Lista de Produtos")]
        public IEnumerable<SelectListItem> ListaProdutos { get; set; }

        public string? JsonProdutos { get; set; }



        //public VendaFormViewModel()
        //{
        //}

        //public VendaFormViewModel(Venda venda)
        //{
        //    if (venda != null)
        //    {
        //        Codigo = venda.Codigo ?? 0;
        //        Data = venda.Data;
        //        CodigoCliente = venda.CodigoCliente;
        //        Total = venda.Total;
        //        Cliente = venda.Cliente;
        //        Produtos = venda.Produtos ?? new List<VendaProdutos>();
        //    }
        //}

        //// Helper: converte viewmodel para entidade Venda para salvar no service
        //public Venda ToEntity()
        //{
        //    return new Venda
        //    {
        //        Codigo = (int?)this.Codigo,
        //        Data = this.Data,
        //        CodigoCliente = this.CodigoCliente ?? 0,
        //        Total = this.Total ?? 0,
        //        Cliente = this.Cliente,
        //        Produtos = this.Produtos
        //    };
        //}
    }
}
