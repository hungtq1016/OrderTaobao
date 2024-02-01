using Infrastructure.EFCore.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration config = builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("gateway.json", optional: false, reloadOnChange: true)
    .Build();

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOcelot(config);
builder.Services.AddSwaggerForOcelot(config);
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerForOcelotUI(opt =>
    {
        opt.PathToSwaggerGenerator = "/swagger/docs";
    });
}

app.UseOcelot().Wait();
app.ConfigureExceptionHandler();

app.Run();
