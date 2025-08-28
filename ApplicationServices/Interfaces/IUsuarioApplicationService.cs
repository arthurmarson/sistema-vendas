using SistemaVenda.Domain.Entities;
using SistemaVenda.Models;

namespace Application.ApplicationServices.Interfaces
{
    public interface IUsuarioApplicationService
    {
        Task<Usuario> AuthenticateAsync(string email, string senha);
    }
}
