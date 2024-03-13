// Add services to the container.

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
builder.Services.AddSqlServerDbContext<AudioContext>(configuration.GetConnectionString("audioDB"));
builder.Services.AddCustomMapper<AudioProfile>();

builder.Services.AddScoped<IAudioService, CAudioService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(AudioRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

var context = services.GetRequiredService<AudioContext>();

if (context.Database.GetPendingMigrations().Any())
{
    context.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
