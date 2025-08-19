using Microsoft.EntityFrameworkCore;
using SistemaVenda.Entities;

namespace SistemaVenda.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Categoria> Categoria { get; set; } = default!;
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Venda> Venda { get; set; }
        public DbSet<VendaProdutos> VendaProdutos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<VendaProdutos>().HasKey(x => new { x.CodigoVenda, x.CodigoProduto });
            builder.Entity<VendaProdutos>()
                .HasOne(x => x.Venda)
                .WithMany(y => y.Produtos)
                .HasForeignKey(x => x.CodigoVenda);

            builder.Entity<VendaProdutos>()
                .HasOne(x => x.Produto)
                .WithMany(y => y.VendasP)
                .HasForeignKey(x => x.CodigoProduto);
        }
    }
}