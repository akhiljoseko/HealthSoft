using HealthSoft.Core.Entities;
using HealthSoft.Core.RepositoryInterfaces;
using HealthSoft.Infrastructure.Repositories;
using HealthSoft.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("HealthSoftDbContextConnection") ?? throw new InvalidOperationException("Connection string 'HealthSoftDbContextConnection' not found.");
builder.Services.AddDbContext<HealthSoftDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<HealthSoftDbContext>();

// Add services to the container.
builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Seeding Admin user
//using var scope = app.Services.CreateScope();
//var services = scope.ServiceProvider;
//try
//{
//    await IdentitySeeder.SeedAsync(services);
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
