using Domain.Interfaces;
using Domain.Repository;
using SistemaVenda.Domain.Entities;
using System.Data;

namespace Domain.Services
{
    public class CategoriaService : ICategoriaService
    {
        ICategoriaRepository CategoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            CategoriaRepository = categoriaRepository;
        }

        public async Task InsertAsync(Categoria categoria)
        {
            await CategoriaRepository.CreateAsync(categoria);
        }

        public async Task<IEnumerable<Categoria>> FindAllAsync()
        {
            return await CategoriaRepository.ReadAllAsync(); 
        }

        public async Task<Categoria> FindByIdAsync(int id)
        {
            return await CategoriaRepository.ReadAsync(id);
        }

        public async Task UpdateAsync(Categoria categoria)
        {
            await CategoriaRepository.UpdateAsync(categoria);
        }

        public async Task RemoveAsync(int id)
        {
            await CategoriaRepository.DeleteAsync(id);
        }
    }
}
