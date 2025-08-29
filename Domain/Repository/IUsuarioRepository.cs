using SistemaVenda.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> FindByEmailAndPasswordAsync(string email, string senha);
    }
}
