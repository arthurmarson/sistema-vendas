using SistemaVenda.Models;

namespace Application.ApplicationServices.Interfaces
{
    public interface IProdutoApplicationService
    {
        Task InsertAsync(ProdutoFormViewModel produto);

        Task<IEnumerable<ProdutoFormViewModel>> FindAllAsync();

        Task<ProdutoFormViewModel> FindByIdAsync(int codigoProduto);

        Task UpdateAsync(ProdutoFormViewModel produto);

        Task RemoveAsync(int id);

    }
}
