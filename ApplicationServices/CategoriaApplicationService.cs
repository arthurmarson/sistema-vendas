using Application.ApplicationServices.Interfaces;
using Domain.Interfaces;
using Domain.Services;
using SistemaVenda.Domain.Entities;
using SistemaVenda.Models;

namespace Application.ApplicationServices
{
    public class CategoriaApplicationService : ICategoriaApplicationService
    {
        private readonly ICategoriaService _categoriaService;

        // Injeção de dependência do serviço de categoria do domínio
        public CategoriaApplicationService(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        public async Task<IEnumerable<CategoriaViewModel>> FindAllAsync()
        {
            // Chamada ao serviço de domínio para obter todas as categorias
            var lista = await _categoriaService.FindAllAsync();

            List<CategoriaViewModel> listaCategoria = new List<CategoriaViewModel>();

            // Mapeamento dos dados do domínio (Categoria) para a ViewModel (CategoriaViewModel)
            foreach (var item in lista)
            {
                CategoriaViewModel categoria = new CategoriaViewModel()
                {
                    Codigo = item.Codigo,
                    Descricao = item.Descricao
                };
                listaCategoria.Add(categoria);
            }
            return listaCategoria;
        }
    }
}
