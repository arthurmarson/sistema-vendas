using SistemaVenda.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUsuarioService : ICRUDService<Usuario>
    {
        Task<Usuario> FindByEmailAndPasswordAsync(string email, string senha);
    }
}
