using Application.ApplicationServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services.Exceptions;
using SistemaVenda.Models;

namespace SistemaVenda.Controllers
{
    public class ProdutoController : Controller
    {
        readonly IProdutoApplicationService _applicationServiceProduto;
        readonly ICategoriaApplicationService _applicationServiceCategoria; 

        public ProdutoController(IProdutoApplicationService applicationServiceProduto, ICategoriaApplicationService categoriaApplicationService)
        {
            _applicationServiceProduto = applicationServiceProduto;
            _applicationServiceCategoria = categoriaApplicationService; 
        }

        public async Task<IActionResult> Index()
        {
            return View(await _applicationServiceProduto.FindAllAsync());
        }

        public async Task<IActionResult> Cadastro()
        {
            var viewModel = new ProdutoFormViewModel
            {
                Categorias = await _applicationServiceCategoria.FindAllAsync() 
            };

            return View(viewModel);
        }

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

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Código não informado." });
            }
            var obj = await _applicationServiceProduto.FindByIdAsync(id.Value);
            obj.Categorias = await _applicationServiceCategoria.FindAllAsync(); 
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Produto não encontrado." });
            }
            return View(obj);
        }

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
            return View(obj); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                await _applicationServiceProduto.RemoveAsync(id);
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
