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

        public override async Task<IEnumerable<Produto>> ReadAllAsync()
        {
            return await DbSetContext
                .Include(p => p.Categoria) 
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
