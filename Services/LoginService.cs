using Microsoft.EntityFrameworkCore;
using SistemaVenda.DAL;
using SistemaVenda.Entities;
using SistemaVenda.Helpers;

namespace SistemaVenda.Services
{
    public class LoginService
    {
        protected ApplicationDbContext _context;
        

        public LoginService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> AuthenticateAsync(string email, string senha)
        {
            var senhaCriptografada = Criptografia.GetMD5Hash(senha);

            var user = await _context.Usuario.FirstOrDefaultAsync(u => u.Email == email && u.Senha == senhaCriptografada);
            return user; // Retorna o usuário encontrado ou null.
        }
    }
}
