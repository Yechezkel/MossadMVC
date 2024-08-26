using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MossadMVC.Controllers;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

builder.Services.AddScoped<AgentsController>();//////זה יהי מיותר כשאסדר את הקוד לSERVICES

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
    pattern: "{controller=Info}/{action=Index}/{id?}");

app.Run();
