using Microsoft.EntityFrameworkCore;
using HealthSoft.Infrastructure;
using HealthSoft.Core.Entities;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("HealthSoftDbContextConnection") ?? throw new InvalidOperationException("Connection string 'HealthSoftDbContextConnection' not found.");

builder.Services.AddDbContext<HealthSoftDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddRazorPages();
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<HealthSoftDbContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Seeding Admin user
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    await IdentitySeeder.SeedAsync(services);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

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

app.MapRazorPages();

app.Run();
