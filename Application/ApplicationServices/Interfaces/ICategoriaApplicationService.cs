using SistemaVenda.Models;

namespace Application.ApplicationServices.Interfaces
{
    public interface ICategoriaApplicationService
    {
        Task InsertAsync(CategoriaViewModel categoria);

        Task<IEnumerable<CategoriaViewModel>> FindAllAsync();

        Task<CategoriaViewModel> FindByIdAsync(int codigoCategoria);

        Task UpdateAsync(CategoriaViewModel categoria);

        Task RemoveAsync(int id);

    }
}
