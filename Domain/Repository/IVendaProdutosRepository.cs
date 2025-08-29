using SistemaVenda.Domain.DTO;
using SistemaVenda.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IVendaProdutosRepository 
    {
        IEnumerable<RelatorioViewModel> ListaRelatorio();
    }
}
