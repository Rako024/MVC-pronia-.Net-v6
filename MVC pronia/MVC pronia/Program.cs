using Business.Services.Abstracts;
using Business.Services.Concretes;
using Core.RepositoryAbstracts;
using Data.DAL;
using Data.RepositoryConcretes;
using Microsoft.EntityFrameworkCore;

namespace MVC_pronia
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<ISliderRepository,SliderRepository>();   
            builder.Services.AddScoped<ISliderService,SliderService>();
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });
            var app = builder.Build();

            app.UseStaticFiles();
            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
            app.MapControllerRoute("Default", "{controller=home}/{action=index}/{id?}");
            app.Run();
        }
    }
}
