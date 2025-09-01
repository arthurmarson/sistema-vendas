using Application.ApplicationServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace SistemaVenda.Controllers
{
    public class RelatorioController : Controller
    {
        protected IVendaApplicationService _vendaApplicationService;

        public RelatorioController(IVendaApplicationService vendaApplicationService)
        {
            _vendaApplicationService = vendaApplicationService;
        }

        public async Task<IActionResult> Index()
        {
            var listaVendaProduto = _vendaApplicationService.ListaRelatorio();

            if (listaVendaProduto == null || !listaVendaProduto.Any())
            {
                ViewBag.Valores = "[]";
                ViewBag.Labels = "[]";
                ViewBag.Cores = "[]";
                return View();
            }

            string valores = string.Join(", ", listaVendaProduto.Select(x => x.TotalVendido));
            string labels = string.Join(", ", listaVendaProduto.Select(x => $"'{x.Descricao}'"));
            string cores = string.Join(", ", listaVendaProduto.Select((x, i) => $"' rgb({i * 50 % 255}, {i * 100 % 255}, {i * 150 % 255}) '"));

            ViewBag.Valores = valores;
            ViewBag.Labels = labels;
            ViewBag.Cores = cores;

            return View();
        }
    }
}
