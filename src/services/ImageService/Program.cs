using ImageService.Data;
using ImageService.Infrastructure.Features;
using ImageService.Infrastructure;
using Infrastructure.EFCore.Repository;
using Infrastructure.EFCore.Extensions;
using ImageService.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddMemoryCache();

builder.Services.AddJWT(configuration);
builder.Services.AddSqlServerDbContext<ImageContext>(configuration.GetConnectionString("imageDB.docker"));
builder.Services.AddCustomMapper<ImageProfile>();

builder.Services.AddScoped<IRepository<Image>, ImageRepository>();
builder.Services.AddScoped<IImageService, CImageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

var context = services.GetRequiredService<ImageContext>();

if (context.Database.GetPendingMigrations().Any())
{
    context.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
