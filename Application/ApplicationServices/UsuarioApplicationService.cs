using Application.ApplicationServices.Interfaces;
using Domain.Interfaces;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using SistemaVenda.Domain.Entities;
using SistemaVenda.Helpers;
using SistemaVenda.Models;

namespace Application.ApplicationServices
{
    public class UsuarioApplicationService : IUsuarioApplicationService
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioApplicationService(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<Usuario> AuthenticateAsync(string email, string senha)
        {
            var senhaCriptografada = Criptografia.GetMD5Hash(senha);

            var user = await _usuarioService.FindByEmailAndPasswordAsync(email, senhaCriptografada);
            return user; 
        }
    }
}
