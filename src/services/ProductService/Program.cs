using ProductService.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddMemoryCache();
builder.Services.AddCors();
builder.Services.AddJWT(configuration);
builder.Services.AddSqlServerDbContext<ProductContext>(configuration.GetConnectionString("productDB.docker"));
builder.Services.AddCustomMapper<ProductProfile>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(ProductRepository<>));
builder.Services.AddScoped<IProductService, BaseProductService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

var context = services.GetRequiredService<ProductContext>();

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

