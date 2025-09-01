using SistemaVenda.Domain.Entities;
using System.Collections.Generic;

namespace Domain.Repository
{
    public interface IVendaRepository : IRepository<Venda>
    {
        Task<IEnumerable<Cliente>> ObterListaClientesAsync();
        Task<IEnumerable<Produto>> ObterListaProdutosAsync();
    }
}
