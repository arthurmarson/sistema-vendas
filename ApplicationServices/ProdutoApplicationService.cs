using Application.ApplicationServices.Interfaces;
using Domain.Interfaces;
using Domain.Services;
using SistemaVenda.Domain.Entities;
using SistemaVenda.Models;

namespace Application.ApplicationServices
{
    public class ProdutoApplicationService : IProdutoApplicationService
    {
        private readonly IProdutoService _produtoService;

        public ProdutoApplicationService(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }


        public async Task InsertAsync(ProdutoFormViewModel produto)
        {
            Produto item = new Produto()
            {
                Codigo = produto.Codigo,
                Descricao = produto.Descricao,
                Quantidade = produto.Quantidade,
                Valor = (decimal)produto.Valor,
                CodigoCategoria = (int)produto.CodigoCategoria,
                Categoria = produto.Categoria != null
                    ? new Categoria
                    {
                        Codigo = produto.Categoria.Codigo,
                        Descricao = produto.Categoria.Descricao
                    }
                    : null
            };
            await _produtoService.InsertAsync(item);
        }

        public async Task<IEnumerable<ProdutoFormViewModel>> FindAllAsync()
        {
            var lista = await _produtoService.FindAllAsync();

            return lista.Select(item => new ProdutoFormViewModel
            {
                Codigo = item.Codigo,
                Descricao = item.Descricao,
                Quantidade = item.Quantidade,
                Valor = item.Valor,
                CodigoCategoria = item.CodigoCategoria,
                Categoria = item.Categoria != null
                    ? new CategoriaViewModel
                    {
                        Codigo = item.Categoria != null ? item.Categoria.Codigo : 0,
                        Descricao = item.Categoria.Descricao
                    }
                    : null
            }).ToList();
        }

        public async Task<ProdutoFormViewModel> FindByIdAsync(int codigoProduto)
        {
            var registro = await _produtoService.FindByIdAsync(codigoProduto);

            ProdutoFormViewModel produto = new ProdutoFormViewModel()
            {
                Codigo = registro.Codigo,
                Descricao = registro.Descricao,
                Quantidade = registro.Quantidade,
                Valor = (decimal)registro.Valor,
                CodigoCategoria = (int)registro.CodigoCategoria
            };

            return produto;
        }

        public async Task UpdateAsync(ProdutoFormViewModel produto)
        {
            Produto item = new Produto()
            {
                Codigo = produto.Codigo,
                Descricao = produto.Descricao,
                Quantidade = produto.Quantidade,
                Valor = (decimal)produto.Valor,
                CodigoCategoria = (int)produto.CodigoCategoria,
                Categoria = produto.Categoria != null
                    ? new Categoria
                    {
                        Codigo = produto.Categoria.Codigo,
                        Descricao = produto.Categoria.Descricao
                    }
                    : null
            };
            await _produtoService.UpdateAsync(item);
        }

        public async Task RemoveAsync(int id)
        {
            await _produtoService.RemoveAsync(id);
        }
    }
}
