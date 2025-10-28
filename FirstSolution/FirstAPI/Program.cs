using FirstAPI.Contexts;
using FirstAPI.Interfaces;
using FirstAPI.Models;
using FirstAPI.Repositories;
using FirstAPI.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region DbContext
builder.Services.AddDbContext<ShoppingContext28Oct25>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
#endregion

#region mapper
builder.Services.AddAutoMapper(typeof(Product));
#endregion

#region Repositories
builder.Services.AddScoped<IRepository<int, Product>, ProductRepositoryDB>();
#endregion


#region Services
builder.Services.AddScoped<IProductService, ProductService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
