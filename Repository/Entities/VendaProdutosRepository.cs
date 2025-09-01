using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using SistemaVenda.Domain.DTO;
using SistemaVenda.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class VendaProdutosRepository : DbContext, IVendaProdutosRepository
    {
        protected ApplicationDbContext DbSetContext;

        public VendaProdutosRepository(ApplicationDbContext mContext)
        {
            DbSetContext = mContext;
        }

        public async Task<IEnumerable<RelatorioViewModel>> ListaRelatorioAsync()
        {
            return await DbSetContext.VendaProdutos
                .Include(x => x.Produto)
                .GroupBy(x => x.CodigoProduto)
                .Select(y => new RelatorioViewModel
                {
                    CodigoProduto = y.Key,
                    Descricao = y.Select(p => p.Produto.Descricao).FirstOrDefault() ?? string.Empty,
                    TotalVendido = y.Sum(p => p.Quantidade),
                })
                .ToListAsync();
        }
    }
}
