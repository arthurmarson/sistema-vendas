using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using SalesWebMvc.Services.Exceptions;
using SistemaVenda.DAL;
using SistemaVenda.Entities;
using System.Data;

namespace SistemaVenda.Services
{
    public class CategoriaService
    {
        protected ApplicationDbContext _context;

        public CategoriaService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Categoria>> FindAllAsync()
        {
            return await _context.Categoria.OrderBy(x => x.Descricao).ToListAsync();
        }

        public async Task<Categoria> FindByIdAsync(int id)
        {
            return await _context.Categoria.FirstOrDefaultAsync(c => c.Codigo == id);
        }

        public async Task InsertAsync(Categoria obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Categoria categoria)
        {
            bool exists = await _context.Categoria.AnyAsync(c => c.Codigo == categoria.Codigo);
            if (!exists)
            {
                throw new NotFoundException("Categoria não encontrada.");
            }

            try
            {
                _context.Update(categoria);
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
                var obj = await _context.Categoria.FindAsync(id);
                _context.Categoria.Remove(obj);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }
    }
}
