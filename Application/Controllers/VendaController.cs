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

        // GET: Venda
        public async Task<IActionResult> Index()
        {
            return View(await _applicationServiceVenda.FindAllAsync());
        }

        // GET: Venda/Cadastro
        public async Task<IActionResult> Cadastro(int? id)
        {
            VendaFormViewModel viewModel = new VendaFormViewModel();

            if (id.HasValue)
            {

                 viewModel = await _applicationServiceVenda.FindByIdAsync(id.Value);
            }

            viewModel.ListaClientes = _applicationServiceVenda.ListaClientes();
            viewModel.ListaProdutos = _applicationServiceVenda.ListaProdutos();

            return View(viewModel);
        }

        // POST: Venda/Cadastro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastro(VendaFormViewModel venda)
        {
            // Remove these properties from ModelState validation
            ModelState.Remove("ListaClientes");
            ModelState.Remove("ListaProdutos");
            if (!ModelState.IsValid)
            {
                venda.ListaClientes = _applicationServiceVenda.ListaClientes();
                venda.ListaProdutos = _applicationServiceVenda.ListaProdutos();
                return View(venda);
            }

            if (venda.Codigo == null)
            {
                await _applicationServiceVenda.InsertAsync(venda);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Venda/Editar/5
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

            // Preencher o ViewModel com os dados da venda
            VendaFormViewModel viewModel = new VendaFormViewModel
            {
                Codigo = venda.Codigo,
                Data = venda.Data,
                CodigoCliente = venda.CodigoCliente,
                Total = venda.Total,
                ListaClientes = _applicationServiceVenda.ListaClientes(),
                ListaProdutos = _applicationServiceVenda.ListaProdutos(),
                JsonProdutos = venda.JsonProdutos // Certifique-se de que JsonProdutos está sendo retornado corretamente
            };

            return View(viewModel);
        }

        // POST: Venda/Editar/5
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

        // GET: Venda/Deletar/5
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

        // POST: Venda/Deletar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                await _applicationServiceVenda.RemoveAsync(id);
                return RedirectToAction(nameof(Index)); // Redirect to the Index action after deletion
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
