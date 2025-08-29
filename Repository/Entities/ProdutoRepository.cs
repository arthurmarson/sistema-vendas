using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using SistemaVenda.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        
        public ProdutoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
           
        }

        public override IEnumerable<Produto> Read()
        {
            return DbSetContext
                .Include(p => p.Categoria) // Inclui a propriedade de navegação Categoria
                .AsNoTracking()
                .ToList();
        }
    }
}
