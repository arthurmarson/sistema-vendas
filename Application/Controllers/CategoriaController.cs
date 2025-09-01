using Application.ApplicationServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services.Exceptions;
using SistemaVenda.Models;

namespace SistemaVenda.Controllers
{
    public class CategoriaController : Controller
    {
        readonly ICategoriaApplicationService _applicationServiceCategoria;

        public CategoriaController(ICategoriaApplicationService applicationServiceCategoria)
        {
            _applicationServiceCategoria = applicationServiceCategoria;
        }

        // GET: Categoria
        public async Task<IActionResult> Index()
        {
            return View(await _applicationServiceCategoria.FindAllAsync());
        }

        // GET: Categoria/Cadastro
        public IActionResult Cadastro()
        {
            return View();
        }

        // POST: Categoria/Cadastro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastro(CategoriaViewModel categoria)
        {
            ModelState.Remove("Produtos");
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }
            await _applicationServiceCategoria.InsertAsync(categoria);
            return RedirectToAction(nameof(Index));
        }

        // GET: Categoria/Editar/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Código não informado." });
            }
            var obj = await _applicationServiceCategoria.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Categoria não encontrada." });
            }
            return View(obj);
        }

        // POST: Categoria/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, CategoriaViewModel categoria)
        {
            ModelState.Remove("Produtos");
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }
            if (id != categoria.Codigo)
            {
                return RedirectToAction(nameof(Error), new { message = "Código inconsistente." });
            }
            try
            {
                await _applicationServiceCategoria.UpdateAsync(categoria);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        // GET: Categoria/Deletar/5
        public async Task<IActionResult> Deletar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Código não informado." });
            }
            var obj = await _applicationServiceCategoria.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Código não encontrado." });
            }
            return View(obj); // If found, return the view with the seller object
        }

        // POST: Categoria/Deletar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                await _applicationServiceCategoria.RemoveAsync(id);
                return RedirectToAction(nameof(Index)); // Redirect to the Index action after deletion
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            return View(new ErrorViewModel { Message = message });
        }

    }
}
