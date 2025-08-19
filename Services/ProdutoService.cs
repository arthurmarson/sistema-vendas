using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using SalesWebMvc.Services.Exceptions;
using SistemaVenda.DAL;
using SistemaVenda.Entities;
using System.Data;

namespace SistemaVenda.Services
{
    public class ProdutoService
    {
        protected ApplicationDbContext _context;

        public ProdutoService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Produto>> FindAllAsync()
        {
            return await _context.Produto
                .Include(p => p.Categoria)
                .OrderBy(x => x.Codigo)
                .ToListAsync();
        }

        public async Task<Produto> FindByIdAsync(int id)
        {
            return await _context.Produto
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(c => c.Codigo == id);
        }

        public async Task InsertAsync(Produto obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Produto produto)
        {
            bool exists = await _context.Produto.AnyAsync(c => c.Codigo == produto.Codigo);
            if (!exists)
            {
                throw new NotFoundException("Produto não encontrado.");
            }

            try
            {
                _context.Update(produto);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Produto.FindAsync(id);
                _context.Produto.Remove(obj);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }
    }
}
