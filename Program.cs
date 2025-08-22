using Application.ApplicationServices;
using Application.ApplicationServices.Interfaces;
using Domain.Interfaces;
using Domain.Repository;
using Domain.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using SistemaVenda.DAL;
using SistemaVenda.Services;
using System.Globalization;

namespace SistemaVenda
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Ficara por enquanto pois o projeto ainda não foi todo migrado para DDD
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("ApplicationDbContext"),
                    new MySqlServerVersion(new Version(8, 0, 36)),
                    builder => builder.MigrationsAssembly("SistemaVenda")
                ));

            // A princípio, será definitiva
            builder.Services.AddDbContext<Repository.Context.ApplicationDbContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("ApplicationDbContext"),
                    new MySqlServerVersion(new Version(8, 0, 36)),
                    builder => builder.MigrationsAssembly("SistemaVenda")
                ));

            //builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSession();

            builder.Services.AddScoped<ClienteService>();
            builder.Services.AddScoped<ProdutoService>();
            builder.Services.AddScoped<VendaService>();
            builder.Services.AddScoped<RelatorioService>();
            builder.Services.AddScoped<LoginService>();

            // Application Service
            builder.Services.AddScoped<ICategoriaApplicationService, CategoriaApplicationService>();

            // Domain Service 
            builder.Services.AddScoped<ICategoriaService, CategoriaService>();

            // Repository
            builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            // Add after other service configurations but before app.UseRouting()
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
