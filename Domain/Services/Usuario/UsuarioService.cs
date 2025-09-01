using Domain.Interfaces;
using Domain.Repository;
using SistemaVenda.Domain.Entities;
using System.Data;

namespace Domain.Services
{
    public class UsuarioService : IUsuarioService
    {
        IUsuarioRepository UsuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            UsuarioRepository = usuarioRepository;
        }

        public async Task InsertAsync(Usuario usuario)
        {
            await UsuarioRepository.CreateAsync(usuario);
        }

        public async Task<IEnumerable<Usuario>> FindAllAsync()
        {
            return await UsuarioRepository.ReadAllAsync(); 
        }

        public async Task<Usuario> FindByIdAsync(int id)
        {
            return await UsuarioRepository.ReadAsync(id);
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            await UsuarioRepository.UpdateAsync(usuario);
        }

        public async Task RemoveAsync(int id)
        {
            await UsuarioRepository.DeleteAsync(id);
        }

        public async Task<Usuario> FindByEmailAndPasswordAsync(string email, string senha)
        {
            return await UsuarioRepository.FindByEmailAndPasswordAsync(email, senha);
        }
    }
}
