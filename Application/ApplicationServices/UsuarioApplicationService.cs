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

        // Injeção de dependência do serviço de categoria do domínio
        public UsuarioApplicationService(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<Usuario> AuthenticateAsync(string email, string senha)
        {
            // Criptografar a senha fornecida pelo usuário
            var senhaCriptografada = Criptografia.GetMD5Hash(senha);

            // Buscar o usuário com a senha criptografada
            var user = await _usuarioService.FindByEmailAndPasswordAsync(email, senhaCriptografada);
            return user; // Retorna o usuário encontrado ou null.
        }
    }
}
