using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using SalesWebMvc.Services.Exceptions;
using SistemaVenda.DAL;
using SistemaVenda.Entities;
using System.Data;

namespace SistemaVenda.Services
{
    public class ClienteService
    {
        protected ApplicationDbContext _context;

        public ClienteService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Cliente>> FindAllAsync()
        {
            return await _context.Cliente.OrderBy(x => x.Nome).ToListAsync();
        }

        public async Task<Cliente> FindByIdAsync(int id)
        {
            return await _context.Cliente.FirstOrDefaultAsync(c => c.Codigo == id);
        }

        public async Task InsertAsync(Cliente obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            bool exists = await _context.Cliente.AnyAsync(c => c.Codigo == cliente.Codigo);
            if (!exists)
            {
                throw new NotFoundException("Cliente não encontrado.");
            }

            try
            {
                _context.Update(cliente);
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
                var obj = await _context.Cliente.FindAsync(id);
                _context.Cliente.Remove(obj);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }
    }
}
