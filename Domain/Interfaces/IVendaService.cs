using SistemaVenda.Domain.DTO;
using SistemaVenda.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IVendaService : ICRUDService<Venda>
    {
        IEnumerable<Cliente> ListaClientes();
        IEnumerable<Produto> ListaProdutos();
        IEnumerable<RelatorioViewModel> ListaRelatorio(); 
    }
}
