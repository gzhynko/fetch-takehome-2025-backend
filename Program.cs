using Microsoft.EntityFrameworkCore;
using PointsApp.Data;
using PointsApp.Services;

var builder = WebApplication.CreateBuilder(args);

// add services to the container
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(opt => 
    opt.UseInMemoryDatabase("ApplicationDbContext"));

builder.Services.AddScoped<IPointTransactionRepository, PointTransactionRepository>();
builder.Services.AddScoped<IPointsService, PointsService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultControllerRoute();

// enable swagger when in dev build
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();
