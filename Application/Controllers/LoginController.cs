using Application.ApplicationServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SistemaVenda.Helpers;
using SistemaVenda.Models;

namespace SistemaVenda.Controllers
{
    public class LoginController : Controller
    {
        protected IUsuarioApplicationService _usuarioApplicationService;
        protected IHttpContextAccessor _httpContextAccessor;

        public LoginController(IUsuarioApplicationService usuarioApplicationService, IHttpContextAccessor httpContextAccessor)
        {
            _usuarioApplicationService = usuarioApplicationService;
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
            ModelState.Remove("Nome"); 
            ModelState.Remove("Codigo"); 
            ViewData["ErrorMessage"] = string.Empty;

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var usuario = await _usuarioApplicationService.AuthenticateAsync(viewModel.Email, viewModel.Senha);
            if (usuario != null)
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext != null)
                {
                    httpContext.Session.SetString(Sessao.NOME_USUARIO, usuario.Nome); 
                    httpContext.Session.SetString(Sessao.EMAIL_USUARIO, usuario.Email);
                    httpContext.Session.SetInt32(Sessao.CODIGO_USUARIO, usuario.Codigo);
                    httpContext.Session.SetInt32(Sessao.LOGADO, 1);
                }

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
