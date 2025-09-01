using Domain.Interfaces;
using Domain.Repository;
using SistemaVenda.Domain.Entities;
using System.Data;

namespace Domain.Services
{
    public class ProdutoService : IProdutoService
    {
        IProdutoRepository ProdutoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            ProdutoRepository = produtoRepository;
        }

        public async Task InsertAsync(Produto produto)
        {
            await ProdutoRepository.CreateAsync(produto);
        }

        public async Task<IEnumerable<Produto>> FindAllAsync()
        {
            return await ProdutoRepository.ReadAllAsync(); 
        }

        public async Task<Produto> FindByIdAsync(int id)
        {
            return await ProdutoRepository.ReadAsync(id);
        }

        public async Task UpdateAsync(Produto produto)
        {
            await ProdutoRepository.UpdateAsync(produto);
        }

        public async Task RemoveAsync(int id)
        {
            await ProdutoRepository.DeleteAsync(id);
        }

    }
}
