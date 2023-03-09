using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using STUDENTSDB.Controllers;
using System.Data.SqlClient;

namespace STUDENTSDB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Retrieve the connection string from the configuration file
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            using var connection = new SqlConnection(connectionString);

            // Register the SqlConnection object with the dependency injection container
            builder.Services.AddSingleton<SqlConnection>(connection);
            builder.Services.AddTransient<IStudentsRepository, StudentsRepository>(provider =>
                new StudentsRepository(connectionString));

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
                pattern: "{controller=Students}/{action=INDEX}/{id?}");

            app.Run();
        }
    }
}