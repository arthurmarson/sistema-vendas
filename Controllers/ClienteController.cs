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
    public class ClienteController : Controller
    {
        protected ApplicationDbContext _context;
        protected ClienteService _clienteService;

        public ClienteController(ApplicationDbContext context, ClienteService clienteService)
        {
            _context = context;
            _clienteService = clienteService;
        }

        // GET: Cliente
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cliente.ToListAsync());
        }

        // GET: Cliente/Cadastro
        public IActionResult Cadastro()
        {
            var viewModel = new ClienteFormViewModel();
            return View(viewModel);
        }

        // POST: Cliente/Cadastro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastro(ClienteFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var clienteEntity = viewModel.ToEntity();
            await _clienteService.InsertAsync(clienteEntity);
            return RedirectToAction(nameof(Index));
        }

        // GET: Cliente/Editar/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Código não informado." });
            }
            var obj = await _clienteService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Cliente não encontrado." });
            }

            var vm = new ClienteFormViewModel(obj);
            return View(vm);
        }

        // POST: Cliente/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, ClienteFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            if (id != viewModel.Codigo)
            {
                return RedirectToAction(nameof(Error), new { message = "Código inconsistente." });
            }
            try
            {
                var clienteToUpdate = viewModel.ToEntity();
                await _clienteService.UpdateAsync(clienteToUpdate);
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

        // GET: Cliente/Deletar/5
        public async Task<IActionResult> Deletar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Código não informado." });
            }
            var obj = await _clienteService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Código não encontrado." });
            }
            return View(obj);
        }

        // POST: Cliente/Deletar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                await _clienteService.RemoveAsync(id);
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
