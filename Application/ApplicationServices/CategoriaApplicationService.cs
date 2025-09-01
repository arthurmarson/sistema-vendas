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


        public async Task InsertAsync(CategoriaViewModel categoria)
        {
            Categoria item = new Categoria()
            {
                Codigo = categoria.Codigo,
                Descricao = categoria.Descricao
            };
            await _categoriaService.InsertAsync(item);
        }

        public async Task<IEnumerable<CategoriaViewModel>> FindAllAsync()
        {
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

        public async Task<CategoriaViewModel> FindByIdAsync(int codigoCategoria)
        {
            var registro = await _categoriaService.FindByIdAsync(codigoCategoria);

            // Mapeamento manual dos dados do domínio (Categoria) para a ViewModel (CategoriaViewModel)
            CategoriaViewModel categoria = new CategoriaViewModel()
            {
                Codigo = registro.Codigo,
                Descricao = registro.Descricao
            };

            return categoria;
        }

        public async Task UpdateAsync(CategoriaViewModel categoria)
        {
            Categoria item = new Categoria()
            {
                Codigo = (int)categoria.Codigo,
                Descricao = categoria.Descricao
            };
            await _categoriaService.UpdateAsync(item);
        }

        public async Task RemoveAsync(int id)
        {
            await _categoriaService.RemoveAsync(id);
        }
    }
}
