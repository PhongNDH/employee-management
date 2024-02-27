using EmployeeManagement.DataAccess.Data;
using EmployeeManagement.Models.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.DataAccess.Repository;
using EmployeeManagement.DataAccess.Service;
using FluentValidation;
using FluentValidation.AspNetCore;
using EmployeeManagement.Models;
using EmployeeManagement.DataAccess.Validation;
using EmployeeManagement.Models.Entity;
using EmployeeManagement.Models.Interface.Service;

namespace EmployeeManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            
            builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
            ));

            //Repository
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //Service
            builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

            //Fluent Validation
            builder.Services.AddMvc();
            builder.Services.AddScoped(typeof(IValidator<>), typeof(GenericValidator<>));
            builder.Services.AddScoped<IValidator<Employee>, EmployeeValidator>();
            builder.Services.AddScoped<IValidator<Province>, ProvinceValidator>();
            builder.Services.AddScoped<IValidator<District>, DistrictValidator>();
            builder.Services.AddScoped<IValidator<Commune>, CommuneValidator>();
            builder.Services.AddScoped<IValidator<Diploma>, DiplomaValidator>();
            builder.Services.AddScoped<IValidator<AwardDiploma>, AwardDiplomaValidator>();
            
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();

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