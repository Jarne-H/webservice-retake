using Microsoft.EntityFrameworkCore;
using PD4ExamAPI.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var allowedOrigins = new[]
{
    "http://www.pd4-examsite.com",
    "https://localhost", "http://localhost",
    "http://www.pd4-examsite.com",
    "http://127.0.0.1",
    "http://www.pd4-examwebservice.com",
    "http://www.pd4-examsite.com"
};

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors((options) =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder/*.WithOrigins(allowedOrigins)*/
              .AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddDbContext<MazeGameContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("mazeconstr")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
