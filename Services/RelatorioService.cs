using Microsoft.EntityFrameworkCore;
using SistemaVenda.DAL;
using SistemaVenda.Entities;
using SistemaVenda.Models;
using System.Collections;

namespace SistemaVenda.Services
{
    public class RelatorioService
    {
        protected ApplicationDbContext _context;

        public RelatorioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RelatorioViewModel>> FindAllAsync()
        {
            return await _context.VendaProdutos
                .GroupBy(x => x.CodigoProduto)
                .Select(y => new RelatorioViewModel
                {
                    CodigoProduto = y.Key,
                    Descricao = y.Select(p => p.Produto.Descricao).FirstOrDefault() ?? string.Empty,
                    TotalVendido = y.Sum(p => p.Quantidade),
                })
                .ToListAsync();
        }

        public void PercorreListaAsync(ICollection lista)
        {
            string valores = string.Empty;
            string labels = string.Empty;
            string cores = string.Empty;

            int i = 0;
            foreach (var obj in lista)
            {
                var item = (RelatorioViewModel)obj;
                valores += item.TotalVendido.ToString() + ", ";
                labels += "'" + item.Descricao + "',";
                cores += $"rgb({i * 50 % 255}, {i * 100 % 255}, {i * 150 % 255}),";
                i++;
            }
        }
    }
}
