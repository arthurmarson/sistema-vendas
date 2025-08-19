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
    public class ProdutoController : Controller
    {
        protected ProdutoService _produtoService;
        protected CategoriaService _categoriaService;

        public ProdutoController(ProdutoService produtoService, CategoriaService categoriaService)
        {
            _produtoService = produtoService;
            _categoriaService = categoriaService;
        }

        // GET: Produto
        public async Task<IActionResult> Index()
        {
            return View(await _produtoService.FindAllAsync());
        }

        // GET: Produto/Cadastro
        public async Task<IActionResult> Cadastro()
        {
            var categorias = await _categoriaService.FindAllAsync();
            var viewModel = new ProdutoFormViewModel { Categorias = categorias };
            return View(viewModel);
        }

        // POST: Produto/Cadastro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastro(ProdutoFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // Recarrega as categorias para o dropdown
                viewModel.Categorias = await _categoriaService.FindAllAsync();
                return View(viewModel);
            }
            var produtoEntity = viewModel.ToEntity();
            await _produtoService.InsertAsync(produtoEntity);
            return RedirectToAction(nameof(Index));
        }

        // GET: Produto/Editar/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Código não informado." });
            }
            var obj = await _produtoService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Produto não encontrado." });
            }

            var vm = new ProdutoFormViewModel(obj);
            vm.Categorias = await _categoriaService.FindAllAsync();
            return View(vm);
        }

        // POST: Produto/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, ProdutoFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categorias = await _categoriaService.FindAllAsync();
                return View(viewModel);
            }
            if (id != viewModel.Codigo)
            {
                return RedirectToAction(nameof(Error), new { message = "Código inconsistente." });
            }
            try
            {
                var produtoToUpdate = viewModel.ToEntity();
                await _produtoService.UpdateAsync(produtoToUpdate);
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

        // GET: Produto/Deletar/5
        public async Task<IActionResult> Deletar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Código não informado." });
            }
            var obj = await _produtoService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Produto não encontrado." });
            }
            return View(obj);
        }

        // POST: Produto/Deletar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                await _produtoService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
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
