using Infrastructure.EFCore.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("gateway.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables(); 

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOcelot(builder.Configuration); 
builder.Services.AddSwaggerForOcelot(builder.Configuration);
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRouting();

app.UseCors(corsPolicyBuilder =>
    corsPolicyBuilder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerForOcelotUI(opt =>
    {
        opt.PathToSwaggerGenerator = "/swagger/docs";
        opt.DownstreamSwaggerEndPointBasePath = "/swagger/docs";
    });
}

app.UseOcelot().Wait();

app.ConfigureExceptionHandler();

app.Run();
