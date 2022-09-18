global using CRUD.Models;
global using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//  資料庫使用: 套件管理主控台指令: Scaffold-DbContext "Server=127.0.0.1;Database=DataBase;Trusted_Connection=true;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir models
//  資料庫存取使用: DataBase First
builder.Services.AddDbContext<DataBaseContext>();

var app = builder.Build();

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
