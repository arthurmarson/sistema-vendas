using Application.ApplicationServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services.Exceptions;
using SistemaVenda.Models;

namespace SistemaVenda.Controllers
{
    public class VendaController : Controller
    {
        readonly IVendaApplicationService _applicationServiceVenda;
        readonly IClienteApplicationService _applicationServiceCliente;
        readonly IProdutoApplicationService _applicationServiceProduto;

        public VendaController(IVendaApplicationService applicationServiceVenda, IClienteApplicationService clienteApplicationService, IProdutoApplicationService produtoApplicationService)
        {
            _applicationServiceVenda = applicationServiceVenda;
            _applicationServiceCliente = clienteApplicationService;
            _applicationServiceProduto = produtoApplicationService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _applicationServiceVenda.FindAllAsync());
        }

        public async Task<IActionResult> Cadastro(int? id)
        {
            VendaFormViewModel viewModel = new VendaFormViewModel();

            if (id.HasValue)
            {

                 viewModel = await _applicationServiceVenda.FindByIdAsync(id.Value);
            }

            viewModel.ListaClientes = await _applicationServiceVenda.ListaClientesAsync();
            viewModel.ListaProdutos = await _applicationServiceVenda.ListaProdutosAsync();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastro(VendaFormViewModel venda)
        {
            ModelState.Remove("ListaClientes");
            ModelState.Remove("ListaProdutos");
            if (!ModelState.IsValid)
            {
                venda.ListaClientes = await _applicationServiceVenda.ListaClientesAsync();
                venda.ListaProdutos = await _applicationServiceVenda.ListaProdutosAsync();
                return View(venda);
            }

            if (venda.Codigo == null)
            {
                await _applicationServiceVenda.InsertAsync(venda);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Código não informado." });
            }

            var venda = await _applicationServiceVenda.FindByIdAsync(id.Value);
            if (venda == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Venda não encontrada." });
            }

            VendaFormViewModel viewModel = new VendaFormViewModel
            {
                Codigo = venda.Codigo,
                Data = venda.Data,
                CodigoCliente = venda.CodigoCliente,
                Total = venda.Total,
                ListaClientes = await _applicationServiceVenda.ListaClientesAsync(),
                ListaProdutos = await _applicationServiceVenda.ListaProdutosAsync(),
                JsonProdutos = venda.JsonProdutos 
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, VendaFormViewModel venda)
        {
            if (!ModelState.IsValid)
            {
                return View(venda);
            }
            if (id != venda.Codigo)
            {
                return RedirectToAction(nameof(Error), new { message = "Código inconsistente." });
            }
            try
            {
                await _applicationServiceVenda.UpdateAsync(venda);
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
            var obj = await _applicationServiceVenda.FindByIdAsync(id.Value);
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
                await _applicationServiceVenda.RemoveAsync(id);
                return RedirectToAction(nameof(Index)); 
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [HttpGet("Venda/LerValorProduto/{CodigoProduto}")]
        public async Task<decimal> LerValorProduto(int CodigoProduto)
        {
            var produto = await _applicationServiceProduto.FindByIdAsync(CodigoProduto);
            return (decimal)(produto != null ? produto.Valor : 0m);
        }

        public IActionResult Error(string message)
        {
            return View(new ErrorViewModel { Message = message });
        }

    }
}
