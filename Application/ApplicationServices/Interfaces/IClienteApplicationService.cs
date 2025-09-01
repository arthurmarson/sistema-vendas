using SistemaVenda.Models;

namespace Application.ApplicationServices.Interfaces
{
    public interface IClienteApplicationService
    {
        Task InsertAsync(ClienteFormViewModel cliente);

        Task<IEnumerable<ClienteFormViewModel>> FindAllAsync();

        Task<ClienteFormViewModel> FindByIdAsync(int codigoCliente);

        Task UpdateAsync(ClienteFormViewModel cliente);

        Task RemoveAsync(int id);

    }
}
