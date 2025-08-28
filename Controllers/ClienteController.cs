using Application.ApplicationServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services.Exceptions;
using SistemaVenda.Models;

namespace SistemaVenda.Controllers
{
    public class ClienteController : Controller
    {
        readonly IClienteApplicationService _applicationServiceCliente;

        public ClienteController(IClienteApplicationService applicationServiceCliente)
        {
            _applicationServiceCliente = applicationServiceCliente;
        }

        // GET: Cliente
        public async Task<IActionResult> Index()
        {
            return View(await _applicationServiceCliente.FindAllAsync());
        }

        // GET: Cliente/Cadastro
        public IActionResult Cadastro()
        {
            return View();
        }

        // POST: Cliente/Cadastro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastro(ClienteFormViewModel cliente)
        {
            if (!ModelState.IsValid)
            {
                return View(cliente);
            }
            await _applicationServiceCliente.InsertAsync(cliente);
            return RedirectToAction(nameof(Index));
        }

        // GET: Cliente/Editar/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Código não informado." });
            }
            var obj = await _applicationServiceCliente.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Categoria não encontrada." });
            }
            return View(obj);
        }

        // POST: Cliente/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, ClienteFormViewModel cliente)
        {
            if (!ModelState.IsValid)
            {
                return View(cliente);
            }
            if (id != cliente.Codigo)
            {
                return RedirectToAction(nameof(Error), new { message = "Código inconsistente." });
            }
            try
            {
                await _applicationServiceCliente.UpdateAsync(cliente);
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
            var obj = await _applicationServiceCliente.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Código não encontrado." });
            }
            return View(obj); // If found, return the view with the seller object
        }

        // POST: Cliente/Deletar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletar(int id)
        {
            try
            {
                await _applicationServiceCliente.RemoveAsync(id);
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
