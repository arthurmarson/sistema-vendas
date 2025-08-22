using SistemaVenda.Models;

namespace Application.ApplicationServices.Interfaces
{
    public interface ICategoriaApplicationService
    {
        Task<IEnumerable<CategoriaViewModel>> FindAllAsync();


    }
}
