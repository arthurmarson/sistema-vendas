using Microsoft.AspNetCore.Mvc;
using SistemaVenda.Models;
using SistemaVenda.Services;
using System.Threading.Tasks;
using System.Linq;

namespace SistemaVenda.Controllers
{
    public class RelatorioController : Controller
    {
        protected RelatorioService _relatorioService;

        public RelatorioController(RelatorioService relatorioService)
        {
            _relatorioService = relatorioService;
        }

        public async Task<IActionResult> Index()
        {
            var listaVendaProduto = await _relatorioService.FindAllAsync();

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
