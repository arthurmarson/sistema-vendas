using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenda.Entities;
using SistemaVenda.Helpers;
using SistemaVenda.Models;
using SistemaVenda.Services;

namespace SistemaVenda.Controllers
{
    public class LoginController : Controller
    {
        protected LoginService _loginService;
        protected IHttpContextAccessor _httpContextAccessor;

        public LoginController(LoginService loginService, IHttpContextAccessor httpContextAccessor)
        {
            _loginService = loginService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index(int? id)
        {
            if (id != null)
            {
                if (id == 0)
                {
                    _httpContextAccessor.HttpContext.Session.Clear();
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginFormViewModel viewModel)
        {
            ModelState.Remove("Nome"); // Remove a validação para o campo Nome
            ModelState.Remove("Codigo"); // Remove a validação para o campo Codigo
            ViewData["ErrorMessage"] = string.Empty;

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var usuario = await _loginService.AuthenticateAsync(viewModel.Email, viewModel.Senha);
            if (usuario != null)
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext != null)
                {
                    httpContext.Session.SetString(Sessao.NOME_USUARIO, usuario.Nome); // Use o nome do usuário retornado
                    httpContext.Session.SetString(Sessao.EMAIL_USUARIO, usuario.Email);
                    httpContext.Session.SetInt32(Sessao.CODIGO_USUARIO, usuario.Codigo ?? 0);
                    httpContext.Session.SetInt32(Sessao.LOGADO, 1);
                }

                // Redireciona para a página Index da controller Home
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["ErrorMessage"] = "Email ou senha não existem no sistema.";
                return View(viewModel);
            }
        }
    }
}
