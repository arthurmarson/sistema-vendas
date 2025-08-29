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
        public IEnumerable<Cliente> ObterListaClientes()
        {
            return Db.Set<Cliente>().ToList();
        }

        public IEnumerable<Produto> ObterListaProdutos()
        {
            return Db.Set<Produto>().ToList();
        }

        public override Venda Read(int Id)
        {
            return DbSetContext
                .Include(v => v.Cliente)
                .Include(v => v.Produtos)
                    .ThenInclude(vp => vp.Produto)
                .FirstOrDefault(v => v.Codigo == Id);
        }

        public override void Delete(int Id)
        {
            var venda = DbSetContext
                .Include(v => v.Produtos) // Incluir os produtos relacionados
                .FirstOrDefault(v => v.Codigo == Id);

            if (venda == null)
            {
                throw new NotFoundException("Venda não encontrada.");
            }

            // Remover os produtos relacionados
            Db.Set<VendaProdutos>().RemoveRange(venda.Produtos);

            // Remover a venda
            DbSetContext.Remove(venda);

            Db.SaveChanges();
        }
    }
}
