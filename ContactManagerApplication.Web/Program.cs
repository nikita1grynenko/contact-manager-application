using ContactManagerApplication.Application.Contracts;
using ContactManagerApplication.Application.Service;
using ContactManagerApplication.Application.Validators;
using ContactManagerApplication.Infrastructure.Context;
using ContactManagerApplication.Infrastructure.Contracts;
using ContactManagerApplication.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerApplication.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Register repositories and services with interfaces for DI
        builder.Services.AddScoped<IContactRepository, ContactRepository>();
        builder.Services.AddScoped<IContactService, ContactService>();

        // Add controllers with views
        builder.Services.AddControllersWithViews();
        builder.Services.AddValidatorsFromAssemblyContaining<ContactValidator>(); 
        builder.Services.AddFluentValidationAutoValidation(); 
        builder.Services.AddFluentValidationClientsideAdapters(); 


        var app = builder.Build();

        // Configure the HTTP request pipeline
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Contact}/{action=Index}/{id?}");

        app.Run();
    }
}