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
            ProdutoRepository.Create(produto);
        }

        public async Task<IEnumerable<Produto>> FindAllAsync()
        {
            return ProdutoRepository.Read(); // Chama o método sobrescrito no ProdutoRepository
        }

        public async Task<Produto> FindByIdAsync(int id)
        {
            return ProdutoRepository.Read(id);
        }

        public async Task UpdateAsync(Produto produto)
        {
            ProdutoRepository.Update(produto);
        }

        public async Task RemoveAsync(int id)
        {
            ProdutoRepository.Delete(id);
        }

    }
}
