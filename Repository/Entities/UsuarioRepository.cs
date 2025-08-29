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
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<Usuario> FindByEmailAndPasswordAsync(string email, string senha)
        {
            return await DbSetContext.FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);
        }
    }

}
