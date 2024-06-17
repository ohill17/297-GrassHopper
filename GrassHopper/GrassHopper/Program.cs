using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using GrassHopper;
using GrassHopper.Data;
using GrassHopper.Data.Repositories;
using GrassHopper.Models;
using System.Configuration;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        o.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

var appSettings = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettings);

builder.Services.AddTransient<IEmailSender, EmailSender>();

var connectionString =
    builder.Configuration.GetConnectionString("MySqlConnection");

Dictionary<string, string> adminInfo = new() 
{ 
    { "AdminUName", builder.Configuration.GetValue<string>("AdminLoginInfo:Username") },
    { "AdminPass", builder.Configuration.GetValue<string>("AdminLoginInfo:Password") },
    { "AdminName", builder.Configuration.GetValue<string>("AdminLoginInfo:Name") }
};

Dictionary<string, string> myAppSettings = new()
{
    { "FacebookAppId", builder.Configuration.GetValue<string>("AppSettings:FacebookAppId") },
    { "FacebookAppSecret", builder.Configuration.GetValue<string>("AppSettings:FacebookAppSecret") },
    { "FacebookRedirectUri", builder.Configuration.GetValue<string>("AppSettings:FacebookRedirectUri") }
};



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


builder.Services.AddTransient<IPhotoRepository, PhotoRepository>();
builder.Services.AddTransient<IReviewRepository, ReviewRepository>();
builder.Services.AddTransient<IPortfolioRepository, PortfolioRepository>();
builder.Services.AddTransient<ITokenRepository, TokenRepository>();
builder.Services.AddTransient<IAppSettingsRepository, AppSettingsRepository>();

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;
});

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    //var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    SeedData.Seed(scope.ServiceProvider, adminInfo, myAppSettings);
}


app.Run();


