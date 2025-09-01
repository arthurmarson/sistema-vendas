using Application.ApplicationServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services.Exceptions;
using SistemaVenda.Models;

namespace SistemaVenda.Controllers
{
    public class ProdutoController : Controller
    {
        readonly IProdutoApplicationService _applicationServiceProduto;
        readonly ICategoriaApplicationService _applicationServiceCategoria; // Dependência adicionada

        public ProdutoController(IProdutoApplicationService applicationServiceProduto, ICategoriaApplicationService categoriaApplicationService)
        {
            _applicationServiceProduto = applicationServiceProduto;
            _applicationServiceCategoria = categoriaApplicationService; // Injeção da dependência
        }

        // GET: Produto
        public async Task<IActionResult> Index()
        {
            return View(await _applicationServiceProduto.FindAllAsync());
        }

        // GET: Produto/Cadastro
        public async Task<IActionResult> Cadastro()
        {
            var viewModel = new ProdutoFormViewModel
            {
                Categorias = await _applicationServiceCategoria.FindAllAsync() // Popula a lista de categorias
            };

            return View(viewModel);
        }

        // POST: Produto/Cadastro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastro(ProdutoFormViewModel produto)
        {
            if (!ModelState.IsValid)
            {
                return View(produto);
            }
            await _applicationServiceProduto.InsertAsync(produto);
            return RedirectToAction(nameof(Index));
        }

        // GET: Produto/Editar/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Código não informado." });
            }
            var obj = await _applicationServiceProduto.FindByIdAsync(id.Value);
            obj.Categorias = await _applicationServiceCategoria.FindAllAsync(); // Popula a lista de categorias
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Produto não encontrado." });
            }
            return View(obj);
        }

        // POST: Produto/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, ProdutoFormViewModel produto)
        {
            if (!ModelState.IsValid)
            {
                return View(produto);
            }
            if (id != produto.Codigo)
            {
                return RedirectToAction(nameof(Error), new { message = "Código inconsistente." });
            }
            try
            {
                await _applicationServiceProduto.UpdateAsync(produto);
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
            var obj = await _applicationServiceProduto.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Código não encontrado." });
            }
            return View(obj); // If found, return the view with the seller object
        }

        // POST: Produto/Deletar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                await _applicationServiceProduto.RemoveAsync(id);
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
