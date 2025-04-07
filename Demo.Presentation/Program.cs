using Demo.DataAccess.Contexts;
using Demo.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace Demo.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Add services to the container

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration["ConnectionStrings : DefaultConnection"]);
                //options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"] );
                //options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStrings"));

            }); // register the service in DI container

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            #endregion

            var app = builder.Build();

            #region Configure the HTTP request pipeline

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
            #endregion

            app.Run();
        }
    }
}
