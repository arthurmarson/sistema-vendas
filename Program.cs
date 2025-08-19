using Microsoft.EntityFrameworkCore;
using SistemaVenda.DAL;
using SistemaVenda.Services;

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

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddSession();

            builder.Services.AddScoped<CategoriaService>();
            builder.Services.AddScoped<ClienteService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
