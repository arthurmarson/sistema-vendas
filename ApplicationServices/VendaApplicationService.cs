using Application.ApplicationServices.Interfaces;
using Domain.Interfaces;
using Domain.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SalesWebMvc.Services.Exceptions;
using SistemaVenda.Domain.Entities;
using SistemaVenda.Models;

namespace Application.ApplicationServices
{
    public class VendaApplicationService : IVendaApplicationService
    {
        private readonly IVendaService _vendaService;

        // Injeção de dependência do serviço de venda do domínio
        public VendaApplicationService(IVendaService vendaService)
        {
            _vendaService = vendaService;
        }

        public async Task InsertAsync(VendaFormViewModel venda)
        {
            if (venda == null)
                throw new ArgumentNullException(nameof(venda));

            Venda item = new Venda()
            {
                Codigo = venda.Codigo ?? 0, 
                Data = venda.Data, 
                CodigoCliente = venda.CodigoCliente ?? throw new InvalidOperationException("Cliente é obrigatório."),
                Total = venda.Total, 
                Produtos = string.IsNullOrWhiteSpace(venda.JsonProdutos)
                    ? new List<VendaProdutos>() // Se JsonProdutos for null ou vazio, inicialize com uma lista vazia
                    : JsonConvert.DeserializeObject<ICollection<VendaProdutos>>(venda.JsonProdutos)
            };

            await _vendaService.InsertAsync(item);
        }

        public async Task<IEnumerable<VendaFormViewModel>> FindAllAsync()
        {
            var lista = await _vendaService.FindAllAsync();

            List<VendaFormViewModel> listaVendas = new List<VendaFormViewModel>();

            // Mapeamento dos dados do domínio (Venda) para a ViewModel (VendaFormViewModel)
            foreach (var item in lista)
            {
                VendaFormViewModel venda = new VendaFormViewModel()
                {
                    Codigo = (int)item.Codigo,
                    Data = (DateTime)item.Data,
                    CodigoCliente = (int)item.CodigoCliente,
                    Total = item.Total
                };
                listaVendas.Add(venda);
            }
            return listaVendas;
        }

        public async Task<VendaFormViewModel> FindByIdAsync(int codigoVenda)
        {
            var registro = await _vendaService.FindByIdAsync(codigoVenda);

            // Mapeamento manual dos dados do domínio (Venda) para a ViewModel (VendaFormViewModel)
            VendaFormViewModel venda = new VendaFormViewModel()
            {
                Codigo = (int)registro.Codigo,
                Data = (DateTime)registro.Data,
                CodigoCliente = (int)registro.CodigoCliente,
                Total = registro.Total,
                JsonProdutos = registro.Produtos != null && registro.Produtos.Any()
                    ? JsonConvert.SerializeObject(registro.Produtos.Select(p => new
                    {
                        p.CodigoProduto,
                        p.Quantidade,
                        p.ValorUnitario,
                        ValorTotal = p.Quantidade * p.ValorUnitario
                    }))
                    : "[]"
            };

            return venda;
        }

        public async Task UpdateAsync(VendaFormViewModel venda)
        {
            Venda entity = new Venda()
            {
                Codigo = (int)venda.Codigo,
                Data = (DateTime)venda.Data,
                CodigoCliente = (int)venda.CodigoCliente,
                Total = venda.Total
            };
            await _vendaService.UpdateAsync(entity);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                await _vendaService.RemoveAsync(id);
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException("Erro ao deletar a venda. Verifique se há dependências relacionadas.", e);
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro inesperado ao tentar deletar a venda.", e);
            }
        }

        public IEnumerable<SelectListItem> ListaClientes()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            lista.Add(new SelectListItem()
            {
                Value = string.Empty,
                Text = string.Empty
            });

            foreach (var item in _vendaService.ListaClientes())
            {
                lista.Add(new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Nome.ToString()
                });
            }
            return lista;
        }

        public IEnumerable<SelectListItem> ListaProdutos()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            lista.Add(new SelectListItem()
            {
                Value = string.Empty,
                Text = string.Empty
            });

            foreach (var item in _vendaService.ListaProdutos())
            {
                lista.Add(new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Descricao.ToString()
                });
            }
            return lista;
        }

        public IEnumerable<SistemaVenda.Domain.DTO.RelatorioViewModel> ListaRelatorio()
        {
            return (IEnumerable<SistemaVenda.Domain.DTO.RelatorioViewModel>)_vendaService.ListaRelatorio();
        }
    }
}
