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
            UsuarioRepository.Create(usuario);
        }

        public async Task<IEnumerable<Usuario>> FindAllAsync()
        {
            // Comunicação com o repositório para buscar todas as categorias
            return UsuarioRepository.Read(); 
        }

        public async Task<Usuario> FindByIdAsync(int id)
        {
            // Comunicação com o repositório para buscar uma categoria por ID
            return UsuarioRepository.Read(id);
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            UsuarioRepository.Update(usuario);
        }

        public async Task RemoveAsync(int id)
        {
            UsuarioRepository.Delete(id);
        }

        public async Task<Usuario> FindByEmailAndPasswordAsync(string email, string senha)
        {
            return await UsuarioRepository.FindByEmailAndPasswordAsync(email, senha);
        }


    }
}
