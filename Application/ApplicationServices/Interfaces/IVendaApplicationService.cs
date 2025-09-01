using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVenda.Models;

namespace Application.ApplicationServices.Interfaces
{
    public interface IVendaApplicationService
    {
        Task InsertAsync(VendaFormViewModel venda);

        Task<IEnumerable<VendaFormViewModel>> FindAllAsync();

        Task<VendaFormViewModel> FindByIdAsync(int codigoVenda);

        Task UpdateAsync(VendaFormViewModel venda);

        Task RemoveAsync(int id);

        Task<IEnumerable<SelectListItem>> ListaClientesAsync();
        Task<IEnumerable<SelectListItem>> ListaProdutosAsync();
        Task<IEnumerable<SistemaVenda.Domain.DTO.RelatorioViewModel>> ListaRelatorioAsync();
    }
}
