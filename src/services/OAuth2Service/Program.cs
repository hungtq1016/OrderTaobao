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

builder.Services.AddScoped<IAuthenService, AuthenService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(OAuth2Repository<>));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

var context = services.GetRequiredService<OAuth2Context>();

if (context.Database.GetPendingMigrations().Any())
{
    context.Database.Migrate();
}
app.UseCors(builder =>
        builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
app.ConfigureExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.Run();

