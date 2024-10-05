using Microsoft.EntityFrameworkCore;
using PointsApp.Data;
using PointsApp.Data.Repositories;
using PointsApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(opt => 
    opt.UseInMemoryDatabase("ApplicationDbContext"));

builder.Services.AddScoped<PointTransactionRepository>();
builder.Services.AddScoped<PointsService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultControllerRoute();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();
