using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemaVenda.DAL;
using SistemaVenda.Entities;
using SistemaVenda.Models;

namespace SistemaVenda.Controllers
{
    public class HomeController : Controller
    {
        protected ApplicationDbContext Repositorio;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext repositorio)
        {
            _logger = logger;
            Repositorio = repositorio;
        }

        public IActionResult Index()
        {
            Categoria objCategoria = Repositorio.Categoria.Where(x => x.Codigo == 1).FirstOrDefault();
            objCategoria.Descricao = "Bebidas";
            Repositorio.Entry(objCategoria).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Repositorio.SaveChanges();

            objCategoria = Repositorio.Categoria.Where(x => x.Codigo == 2).FirstOrDefault();
            Repositorio.Attach(objCategoria); 
            Repositorio.Remove(objCategoria);
            Repositorio.SaveChanges();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
