using Infrastructure.EFCore.Extensions;
using Infrastructure.EFCore.Repository;
using Infrastructure.EFCore.Service;
using Microsoft.EntityFrameworkCore;
using OAuth2Service.Features;
using OAuth2Service.Infrastructure.Data;
using OAuth2Service.Models;
using OAuth2Service.Repository;
using OAuth2Service.Services;

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
builder.Services.AddSqlServerDbContext<OAuth2Context>(configuration.GetConnectionString("oauth2DB"));
builder.Services.AddCustomMapper<OAuth2Profile>();

builder.Services.AddScoped(typeof(IService<,,>), typeof(Service<,,>));
builder.Services.AddScoped<IAuthenService, AuthenService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IRoleService, RoleService>();

/*builder.Services.AddScoped<IRepository<User>, OAuth2Repository<User>>();
builder.Services.AddScoped<IRepository<Role>, OAuth2Repository<Role>>();
builder.Services.AddScoped<IRepository<Permission>, OAuth2Repository<Permission>>();
builder.Services.AddScoped<IRepository<Group>, OAuth2Repository<Group>>();
builder.Services.AddScoped<IRepository<Assignment>, OAuth2Repository<Assignment>>();*/
builder.Services.AddScoped(typeof(IRepository<>), typeof(OAuth2Repository<>));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.Run();

