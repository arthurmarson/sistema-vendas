using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SalesWebMvc.Services.Exceptions;
using SistemaVenda.Entities;
using SistemaVenda.Models;
using SistemaVenda.Services;

namespace SistemaVenda.Controllers
{
    public class VendaController : Controller
    {
        protected VendaService _vendaService;
        protected ClienteService _clienteService;
        protected ProdutoService _produtoService;

        public VendaController(VendaService vendaService, ClienteService clienteService, ProdutoService produtoService)
        {
            _vendaService = vendaService;
            _clienteService = clienteService;
            _produtoService = produtoService;
        }

        // GET: Venda
        public async Task<IActionResult> Index()
        {
            return View(await _vendaService.FindAllAsync());
        }

        // GET: Venda/Cadastro
        public async Task<IActionResult> Cadastro(int? id)
        {
            VendaFormViewModel viewModel = new VendaFormViewModel();
            viewModel.ListaClientes = _vendaService.ListaClientes();
            viewModel.ListaProdutos = _vendaService.ListaProdutos();

            if (id.HasValue)
            {
                var venda = await _vendaService.FindByIdAsync(id.Value);
                if (venda == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Venda não encontrada." });
                }
                viewModel.Codigo = venda.Codigo;
                viewModel.Data = venda.Data;
                viewModel.CodigoCliente = venda.CodigoCliente;
                viewModel.Total = venda.Total;
            }

            return View(viewModel);
        }

        // POST: Venda/Cadastro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastro(VendaFormViewModel viewModel)
        {
            // Remove these properties from ModelState validation
            ModelState.Remove("ListaClientes");
            ModelState.Remove("ListaProdutos");

            if (!ModelState.IsValid)
            {
                viewModel.ListaClientes = _vendaService.ListaClientes();
                viewModel.ListaProdutos = _vendaService.ListaProdutos();
                return View(viewModel);
            }
            Venda objVenda = new Venda()
            {
                Codigo = viewModel.Codigo,
                Data = (DateTime)viewModel.Data,
                CodigoCliente = (int)viewModel.CodigoCliente,
                Total = viewModel.Total,
                Produtos = JsonConvert.DeserializeObject<ICollection<VendaProdutos>>(viewModel.JsonProdutos)
            };
            if (viewModel.Codigo == null)
            {
                await _vendaService.InsertAsync(objVenda);
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

            var venda = await _vendaService.FindByIdAsync(id.Value);
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
                ListaClientes = _vendaService.ListaClientes(),
                ListaProdutos = _vendaService.ListaProdutos(),
                JsonProdutos = JsonConvert.SerializeObject(venda.Produtos.Select(p => new
                {
                    p.CodigoProduto,
                    p.Quantidade,
                    p.ValorUnitario,
                    p.ValorTotal
                }))
            };

            return View(viewModel);
        }

        // POST: Venda/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, VendaFormViewModel viewModel)
        {
            // Remove these properties from ModelState validation
            ModelState.Remove("ListaClientes");
            ModelState.Remove("ListaProdutos");

            if (!ModelState.IsValid)
            {
                viewModel.ListaClientes = _vendaService.ListaClientes();
                viewModel.ListaProdutos = _vendaService.ListaProdutos();
                return View(viewModel);
            }

            if (id != viewModel.Codigo)
            {
                return RedirectToAction(nameof(Error), new { message = "O código da venda informado não corresponde ao código do registro." });
            }

            // Validar e deserializar os produtos
            ICollection<VendaProdutos> produtos = new List<VendaProdutos>();
            if (!string.IsNullOrEmpty(viewModel.JsonProdutos))
            {
                try
                {
                    produtos = JsonConvert.DeserializeObject<ICollection<VendaProdutos>>(viewModel.JsonProdutos);
                }
                catch (Exception ex)
                {
                    return RedirectToAction(nameof(Error), new { message = "Erro ao processar os produtos da venda." });
                }
            }

            try
            {
                // Buscar a venda existente no banco
                var vendaExistente = await _vendaService.FindByIdAsync(id);
                if (vendaExistente == null)
                {
                    return RedirectToAction(nameof(Error), new { message = "Venda não encontrada." });
                }

                // Atualizar os campos da venda
                vendaExistente.Data = (DateTime)viewModel.Data;
                vendaExistente.CodigoCliente = (int)viewModel.CodigoCliente;
                vendaExistente.Total = viewModel.Total;

                // Atualizar os produtos da venda
                await _vendaService.UpdateProdutosAsync(vendaExistente, produtos);

                // Salvar as alterações
                await _vendaService.UpdateAsync(vendaExistente);

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

            var obj = await _vendaService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Venda não encontrada." });
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
                await _vendaService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [HttpGet("Venda/LerValorProduto/{id}")]
        public async Task<IActionResult> LerValorProduto(int id)
        {
            if (id <= 0)
            {
                return Json(new { success = false, message = "Id inválido" });
            }

            try
            {
                var valorProduto = await _vendaService.ProcurarValorProdutoAsync(id);
                return Json(new { success = true, valor = valorProduto });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public IActionResult Error(string message)
        {
            return View(new ErrorViewModel { Message = message });
        }
    }
}
