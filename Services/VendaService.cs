using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;
using SistemaVenda.DAL;
using SistemaVenda.Entities;

namespace SistemaVenda.Services
{
    public class VendaService
    {
        protected ApplicationDbContext _context;

        public VendaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Venda>> FindAllAsync()
        {
            return await _context.Venda
                //.Include(v => v.Cliente)
                //.Include(v => v.Produtos)
                //    .ThenInclude(vp => vp.Produto)
                .OrderBy(x => x.Codigo)
                .ToListAsync();
        }

        public async Task<Venda> FindByIdAsync(int id)
        {
            return await _context.Venda
                .Include(v => v.Cliente)
                .Include(v => v.Produtos)
                    .ThenInclude(vp => vp.Produto)
                .FirstOrDefaultAsync(v => v.Codigo == id);
        }

        public IEnumerable<SelectListItem> ListaClientes()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            lista.Add(new SelectListItem()
            {
                Value = string.Empty,
                Text = string.Empty
            });

            foreach (var item in _context.Cliente.ToList())
            {
                lista.Add(new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Nome.ToString()
                });
            }
            return lista;
        }

        public IEnumerable<SelectListItem> ListaProdutos()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            lista.Add(new SelectListItem()
            {
                Value = string.Empty,
                Text = string.Empty
            });

            foreach (var item in _context.Produto.ToList())
            {
                lista.Add(new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Descricao.ToString()
                });
            }
            return lista;
        }

        public async Task InsertAsync(Venda obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Venda venda)
        {
            bool exists = await _context.Venda.AnyAsync(v => v.Codigo == venda.Codigo);
            if (!exists)
            {
                throw new NotFoundException("Venda não encontrada.");
            }

            try
            {
                _context.Update(venda);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var venda = await _context.Venda
                    .Include(v => v.Produtos) // Incluir os produtos relacionados
                    .FirstOrDefaultAsync(v => v.Codigo == id);

                if (venda == null)
                {
                    throw new NotFoundException("Venda não encontrada.");
                }

                // Remover os produtos relacionados
                _context.VendaProdutos.RemoveRange(venda.Produtos);

                // Remover a venda
                _context.Venda.Remove(venda);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException("Erro ao deletar a venda. Verifique se há dependências relacionadas.", e);
            }
            catch (Exception e)
            {
                throw new Exception("Ocorreu um erro inesperado ao tentar deletar a venda.", e);
            }
        }

        public async Task UpdateProdutosAsync(Venda venda, ICollection<VendaProdutos> novosProdutos)
        {
            // Remover os produtos antigos
            var produtosAntigos = _context.VendaProdutos.Where(vp => vp.CodigoVenda == venda.Codigo);
            _context.VendaProdutos.RemoveRange(produtosAntigos);

            // Adicionar os novos produtos
            foreach (var produto in novosProdutos)
            {
                produto.CodigoVenda = venda.Codigo.Value; // Garantir que o código da venda seja atribuído
                _context.VendaProdutos.Add(produto);
            }

            await _context.SaveChangesAsync();
        }
        public async Task<decimal> ProcurarValorProdutoAsync(int CodigoProduto)
        {
            return await _context.Produto
                .Where(x => x.Codigo == CodigoProduto)
                .Select(x => x.Valor)
                .FirstOrDefaultAsync();
        }
    }
}
