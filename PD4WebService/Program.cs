using Microsoft.EntityFrameworkCore;
using PD4ExamAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVista",
        builder => builder.WithOrigins("https://localhost", "http://localhost", "http://www.pd4-examsite.com", "http://127.0.0.1", "http://www.pd4-examwebservice.com")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials());
});

builder.Services.AddDbContext<MazeGameContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("mazeconstr")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowVista");

app.MapControllers();

app.Run();
