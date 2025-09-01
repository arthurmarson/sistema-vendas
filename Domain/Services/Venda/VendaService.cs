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
            await VendaRepository.CreateAsync(venda);
        }

        public async Task<IEnumerable<Venda>> FindAllAsync()
        {
            return await VendaRepository.ReadAllAsync(); 
        }

        public async Task<Venda> FindByIdAsync(int id)
        {
            return await VendaRepository.ReadAsync(id);
        }

        public async Task UpdateAsync(Venda venda)
        {
            await VendaRepository.UpdateAsync(venda);
        }

        public async Task RemoveAsync(int id)
        {
            await VendaRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Cliente>> ListaClientesAsync()
        {
            return await VendaRepository.ObterListaClientesAsync();
        }

        public async Task<IEnumerable<Produto>> ListaProdutosAsync()
        {
            return await VendaRepository.ObterListaProdutosAsync();
        }

        public async Task<IEnumerable<RelatorioViewModel>> ListaRelatorioAsync()
        {
            return await VendaProdutosRepository.ListaRelatorioAsync();
        }
    }
}
