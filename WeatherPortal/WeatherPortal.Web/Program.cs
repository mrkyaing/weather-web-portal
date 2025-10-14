using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeatherPortal.Data.Data;
using WeatherPortal.Data.Interfaces;
using WeatherPortal.Data.Repositories;
using WeatherPortal.Data.UnitOfWork;
using WeatherPortal.Service.Implements;
using WeatherPortal.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
var config = builder.Configuration;
builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(config.GetConnectionString("Weatherprotal")));
builder.Services.AddRazorPages();
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ITownshipRepository, TownshipRepository>();
builder.Services.AddScoped<ISatelliteRadarImageRepository, SatelliteRadarImageRepository>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();
//builder.Services.AddScoped<IWeatherStationRepository, WeatherStationRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IRegionService, RegionService>();
builder.Services.AddTransient<ICityService, CityService>();
builder.Services.AddTransient<ITownshipService, TownshipService>();
builder.Services.AddTransient<IWeatherStationService, WeatherStationService>();
builder.Services.AddTransient<ISatelliteRadarImageService, SatelliteRadarImageService>();
builder.Services.AddTransient<INewsService, NewsService>();
builder.Services.AddTransient<ISatelliteRadarImageService, SatelliteRadarImageService>();  
builder.Services.AddTransient<IAlertService, AlertService>();
builder.Services.AddTransient<IWeatherReadingService, WeatherReadingService>();
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
app.UseAuthentication();
app.UseAuthorization();
app.UseStatusCodePages(async ctx => {
    if (ctx.HttpContext.Response.StatusCode == 403 || ctx.HttpContext.Response.StatusCode == 404)
    {
        ctx.HttpContext.Response.Redirect("/Home/AccessDenied");//your route that u defined in home controller
    }
});
app.MapControllerRoute(name: "default",pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
