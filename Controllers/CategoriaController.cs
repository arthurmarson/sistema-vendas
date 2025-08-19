using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using SalesWebMvc.Services.Exceptions;
using SistemaVenda.DAL;
using SistemaVenda.Entities;
using SistemaVenda.Models;
using SistemaVenda.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SistemaVenda.Controllers
{
    public class CategoriaController : Controller
    {
        protected ApplicationDbContext _context;
        protected CategoriaService _categoriaService;

        public CategoriaController(ApplicationDbContext context, CategoriaService categoriaService)
        {
            _context = context;
            _categoriaService = categoriaService;
        }

        // GET: Categoria
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categoria.ToListAsync());
        }

        // GET: Categoria/Cadastro
        public IActionResult Cadastro()
        {
            return View();
        }

        // POST: Categoria/Cadastro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastro([Bind("Codigo,Descricao")] Categoria categoria)
        {
            ModelState.Remove("Produtos");
            //if (ModelState.IsValid)
            //{
            //    await _categoriaService.InsertAsync(categoria);
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(categoria);

            if (!ModelState.IsValid)
            {
                var viewModel = new CategoriaViewModel { Categoria = categoria};
                return View(viewModel);
            }
            await _categoriaService.InsertAsync(categoria);
            return RedirectToAction(nameof(Index));
        }

        // GET: Categoria/Editar/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Código não informado." });
            }
            var obj = await _categoriaService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Categoria não encontrada." });
            }
            return View(obj);
        }

        // POST: Categoria/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Categoria categoria)
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
                await _categoriaService.UpdateAsync(categoria);
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

        // GET: Departments/Deletar/5
        public async Task<IActionResult> Deletar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Código não informado." });
            }
            var obj = await _categoriaService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Código não encontrado." });
            }
            return View(obj); // If found, return the view with the seller object
        }

        // POST: Departments/Deletar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                await _categoriaService.RemoveAsync(id);
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
