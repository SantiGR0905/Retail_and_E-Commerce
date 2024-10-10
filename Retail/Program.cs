using Microsoft.EntityFrameworkCore;
using Retail.Context;
using Retail.Repositories;
using Retail.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<RetailDbContext>(options => options.UseSqlServer(conString));

builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IInventoriesRepository, InventoriesRepository>();
builder.Services.AddScoped<IInventoriesService, InventoriesService>();
builder.Services.AddScoped<IPermissionsRepository, PermissionsRepository>();
builder.Services.AddScoped<IPermissionsService, PermissionsService>();
builder.Services.AddScoped<IPermissionsXUsersRepository, PermissionsXUsersRepository>();
builder.Services.AddScoped<IPermissionsXUsersService, PermissionsXUsersService>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ISalesRepository, SalesRepository>();
builder.Services.AddScoped<ISalesService, SalesService>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IUserTypesRepository, UserTypesRepository>();
builder.Services.AddScoped<IUserTypesService, UserTypesService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
