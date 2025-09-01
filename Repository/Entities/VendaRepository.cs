using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using SalesWebMvc.Services.Domain.Exceptions;
using SistemaVenda.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class VendaRepository : Repository<Venda>, IVendaRepository
    {
        
        public VendaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
           
        }
        public async Task<IEnumerable<Cliente>> ObterListaClientesAsync()
        {
            return await Db.Set<Cliente>().ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterListaProdutosAsync()
        {
            return await Db.Set<Produto>().ToListAsync();
        }

        public override async Task<Venda> ReadAsync(int Id)
        {
            return await DbSetContext
                .Include(v => v.Cliente)
                .Include(v => v.Produtos)
                    .ThenInclude(vp => vp.Produto)
                .FirstOrDefaultAsync(v => v.Codigo == Id);
        }

        public override async Task DeleteAsync(int Id)
        {
            var venda = await DbSetContext
                .Include(v => v.Produtos) 
                .FirstOrDefaultAsync(v => v.Codigo == Id);

            if (venda == null)
            {
                throw new NotFoundException("Venda não encontrada.");
            }

            Db.Set<VendaProdutos>().RemoveRange(venda.Produtos);

            DbSetContext.Remove(venda);

            await Db.SaveChangesAsync();
        }
    }
}
