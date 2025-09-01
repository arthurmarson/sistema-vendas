using Application.ApplicationServices;
using Application.ApplicationServices.Interfaces;
using Domain.Interfaces;
using Domain.Repository;
using Domain.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Entities;
using System.Globalization;

namespace SistemaVenda
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("ApplicationDbContext"),
                    new MySqlServerVersion(new Version(8, 0, 36)),
                    builder => builder.MigrationsAssembly("SistemaVenda")
                ));

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSession();


            builder.Services.AddScoped<ICategoriaApplicationService, CategoriaApplicationService>();
            builder.Services.AddScoped<IClienteApplicationService, ClienteApplicationService>();
            builder.Services.AddScoped<IProdutoApplicationService, ProdutoApplicationService>();
            builder.Services.AddScoped<IVendaApplicationService, VendaApplicationService>();
            builder.Services.AddScoped<IUsuarioApplicationService, UsuarioApplicationService>();

            builder.Services.AddScoped<ICategoriaService, CategoriaService>();
            builder.Services.AddScoped<IClienteService, ClienteService>();
            builder.Services.AddScoped<IProdutoService, ProdutoService>();
            builder.Services.AddScoped<IVendaService, VendaService>();
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();

            builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
            builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
            builder.Services.AddScoped<IVendaRepository, VendaRepository>();
            builder.Services.AddScoped<IVendaProdutosRepository, VendaProdutosRepository>();
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            var enUS = new CultureInfo("en-US");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = new List<CultureInfo> { enUS },
                SupportedUICultures = new List<CultureInfo> { enUS }
            };

            app.UseRequestLocalization(localizationOptions);

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
