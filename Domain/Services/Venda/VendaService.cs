using Domain.Interfaces;
using Domain.Repository;
using SistemaVenda.Domain.DTO;
using SistemaVenda.Domain.Entities;
using System.Data;

namespace Domain.Services
{
    public class VendaService : IVendaService
    {
        IVendaRepository VendaRepository;
        IVendaProdutosRepository VendaProdutosRepository;

        public VendaService(IVendaRepository vendaRepository, IVendaProdutosRepository vendaProdutosRepository)
        {
            VendaRepository = vendaRepository;
            VendaProdutosRepository = vendaProdutosRepository;
        }

        public async Task InsertAsync(Venda venda)
        {
            VendaRepository.Create(venda);
        }

        public async Task<IEnumerable<Venda>> FindAllAsync()
        {
            return VendaRepository.Read(); 
        }

        public async Task<Venda> FindByIdAsync(int id)
        {
            return VendaRepository.Read(id);
        }

        public async Task UpdateAsync(Venda venda)
        {
            VendaRepository.Update(venda);
        }

        public async Task RemoveAsync(int id)
        {
            VendaRepository.Delete(id);
        }

        public IEnumerable<Cliente> ListaClientes()
        {
            return VendaRepository.ObterListaClientes();
        }

        public IEnumerable<Produto> ListaProdutos()
        {
            return VendaRepository.ObterListaProdutos();
        }

        public IEnumerable<RelatorioViewModel> ListaRelatorio()
        {
            return VendaProdutosRepository.ListaRelatorio();
        }
    }
}
