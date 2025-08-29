using Domain.Interfaces;
using Domain.Repository;
using SistemaVenda.Domain.Entities;
using System.Data;

namespace Domain.Services
{
    public class CategoriaService : ICategoriaService
    {
        ICategoriaRepository CategoriaRepository;

        // Injeção de dependência do repositório de categoria
        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            CategoriaRepository = categoriaRepository;
        }

        public async Task InsertAsync(Categoria categoria)
        {
            CategoriaRepository.Create(categoria);
        }

        public async Task<IEnumerable<Categoria>> FindAllAsync()
        {
            // Comunicação com o repositório para buscar todas as categorias
            return CategoriaRepository.Read(); 
        }

        public async Task<Categoria> FindByIdAsync(int id)
        {
            // Comunicação com o repositório para buscar uma categoria por ID
            return CategoriaRepository.Read(id);
        }

        public async Task UpdateAsync(Categoria categoria)
        {
            CategoriaRepository.Update(categoria);
        }

        public async Task RemoveAsync(int id)
        {
            CategoriaRepository.Delete(id);
        }


        
    }
}
