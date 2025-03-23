using EBISX_POS.API.Data;
using EBISX_POS.API.Services.Interfaces;
using EBISX_POS.API.Services.Repositories;
using ManagerLibrary.Services.Interface;
using ManagerLibrary.Services.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Get Connection String
var connectionString = builder.Configuration.GetConnectionString("POSConnection");

// ✅ Use Pomelo MySQL Provider
builder.Services.AddDbContext<DataContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


// Add Scope of Interface and Repository
builder.Services.AddScoped<IAuth, AuthRepository>();
builder.Services.AddScoped<IMenu, MenuRepository>();
builder.Services.AddScoped<IOrder, OrderRepository>();
builder.Services.AddScoped<IReport, ReportRepository>();
//builder.Services.AddScoped<IBranch>


// Add CORS
builder.Services.AddCors(options =>
{

    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin() // Allow any URL
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

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
