using SistemaVenda.Domain.Entities;
using System.Collections.Generic;

namespace Domain.Repository
{
    public interface IVendaRepository : IRepository<Venda>
    {
        IEnumerable<Cliente> ObterListaClientes();
        IEnumerable<Produto> ObterListaProdutos();
    }
}
