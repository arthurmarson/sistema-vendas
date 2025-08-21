using Microsoft.AspNetCore.Mvc;

namespace SistemaVenda.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
