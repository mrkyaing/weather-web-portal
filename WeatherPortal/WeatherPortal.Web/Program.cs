using Microsoft.EntityFrameworkCore;
using WeatherPortal.Data.Data;
using WeatherPortal.Data.Interfaces;
using WeatherPortal.Data.Repositories;
using WeatherPortal.Data.UnitOfWork;
using WeatherPortal.Service.Implements;
using WeatherPortal.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(option =>option.UseSqlServer(config.GetConnectionString("Weatherprotal")));

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IRegionRepository,RegionRepository>();
builder.Services.AddScoped<IRegionService,RegionService>();
builder.Services.AddScoped<ICityRepository,CityRepository>();
builder.Services.AddScoped<ICityService,CityService>();
builder.Services.AddScoped<ITownshipRepository, TownshipRepository>();
builder.Services.AddScoped<ITownshipService, TownshipService>();
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
