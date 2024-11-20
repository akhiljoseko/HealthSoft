using HealthSoft.Core.Entities;
using HealthSoft.Core.RepositoryInterfaces;
using HealthSoft.Infrastructure;
using HealthSoft.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'HealthSoftDbContextConnection' not found.");
builder.Services.AddDbContext<HealthSoftDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<HealthSoftDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();

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
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
